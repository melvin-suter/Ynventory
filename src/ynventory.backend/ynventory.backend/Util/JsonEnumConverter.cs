using System.Buffers;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ynventory.Backend.Util
{
    public class JsonEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        private static readonly char[] s_specialChars = new[] { ',', ' ' };
        private static readonly TypeCode s_typeCode = Type.GetTypeCode(typeof(T));
        private static readonly bool s_isEnumSigned = ((int)s_typeCode % 2) == 1;
        private const string ValueSeparator = ", ";

        private readonly JsonNamingPolicy? _namingPolicy;

        private readonly ConcurrentDictionary<ulong, JsonEncodedText> _nameCacheWrite;
        private readonly ConcurrentDictionary<string, T>? _nameCacheRead;

        private const int NameCacheSizeSoftLimit = 64;

        public JsonEnumConverter(JsonSerializerOptions serializerOptions) : this(serializerOptions, null)
        {
        }

        public JsonEnumConverter(JsonSerializerOptions serializerOptions, JsonNamingPolicy? namingPolicy)
        {
            _namingPolicy = namingPolicy;
            _nameCacheWrite = new ConcurrentDictionary<ulong, JsonEncodedText>();

            if (namingPolicy != null)
            {
                _nameCacheRead = new ConcurrentDictionary<string, T>();
            }

            var names = Enum.GetNames<T>();
            var values = Enum.GetValues<T>();

            var encoder = serializerOptions.Encoder;

            for(var i = 0; i < names.Length; i++)
            {
                var value = values[i];
                ulong key = ConvertToUInt64(value);
                var name = names[i];

                var jsonName = FormatJsonName(name, namingPolicy);
                _nameCacheWrite.TryAdd(key, JsonEncodedText.Encode(jsonName, encoder));
                _nameCacheRead?.TryAdd(jsonName, value);

                if (name.IndexOfAny(s_specialChars) > 0)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

#pragma warning disable CS8500
        public override unsafe T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var token = reader.TokenType;

            if (token == JsonTokenType.String)
            {
                if (TryParseEnumCore(ref reader, options, out T value))
                {
                    return value;
                }

                return ReadEnumUsingNamingPolicy(reader.GetString());
            }

            if (token != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            switch (s_typeCode)
            {

                case TypeCode.Int32:
                    if (reader.TryGetInt32(out var int32))
                    {
                        return *(T*)&int32;
                    }
                    break;
                case TypeCode.UInt32:
                    if (reader.TryGetUInt32(out var uint32))
                    {
                        return *(T*)&uint32;
                    }
                    break;
                case TypeCode.UInt64:
                    if (reader.TryGetUInt64(out var uint64))
                    {
                        return *(T*)&uint64;
                    }
                    break;
                case TypeCode.Int64:
                    if (reader.TryGetInt64(out var int64))
                    {
                        return *(T*)&int64;
                    }
                    break;
                case TypeCode.SByte:
                    if (reader.TryGetSByte(out var byte8))
                    {
                        return *(T*)&byte8;
                    }
                    break;
                case TypeCode.Byte:
                    if (reader.TryGetByte(out var ubyte8))
                    {
                        return *(T*)&ubyte8;
                    }
                    break;
                case TypeCode.Int16:
                    if (reader.TryGetInt16(out var int16))
                    {
                        return *(T*)&int16;
                    }
                    break;
                case TypeCode.UInt16:
                    if (reader.TryGetUInt16(out var uint16))
                    {
                        return *(T*)&uint16;
                    }
                    break;
            }

            throw new JsonException();
        }

        public override unsafe void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var key = ConvertToUInt64(value);

            if (_nameCacheWrite.TryGetValue(key, out var formatted))
            {
                writer.WriteStringValue(formatted);
                return;
            }

            var original = value.ToString();

            if (IsValidIdentifier(original))
            {
                if (options.DictionaryKeyPolicy != null)
                {
                    original = FormatJsonName(original, options.DictionaryKeyPolicy);
                    writer.WritePropertyName(original);
                    return;
                }

                original = FormatJsonName(original, _namingPolicy);

                if (_nameCacheWrite.Count < NameCacheSizeSoftLimit)
                {
                    formatted = JsonEncodedText.Encode(original, options.Encoder);
                    writer.WritePropertyName(formatted);
                    _nameCacheWrite.TryAdd(key, formatted);
                }
                else
                {
                    writer.WritePropertyName(original);
                }

                return;
            }

            switch (s_typeCode)
            {
                case TypeCode.Int32:
                    writer.WriteNumberValue(*(int*)&value);
                    break;
                case TypeCode.UInt32:
                    writer.WriteNumberValue(*(uint*)&value);
                    break;
                case TypeCode.UInt64:
                    writer.WriteNumberValue(*(ulong*)&value);
                    break;
                case TypeCode.Int64:
                    writer.WriteNumberValue(*(long*)&value);
                    break;
                case TypeCode.Int16:
                    writer.WriteNumberValue(*(short*)&value);
                    break;
                case TypeCode.UInt16:
                    writer.WriteNumberValue(*(ushort*)&value);
                    break;
                case TypeCode.Byte:
                    writer.WriteNumberValue(*(byte*)&value);
                    break;
                case TypeCode.SByte:
                    writer.WriteNumberValue(*(sbyte*)&value);
                    break;
                default:
                    throw new JsonException();
            }
        }
#pragma warning restore CS8500

        private static ulong ConvertToUInt64(object value)
        {
            Debug.Assert(value is T);
            return s_typeCode switch
            {
                TypeCode.Int32 => (ulong)(int)value,
                TypeCode.UInt32 => (uint)value,
                TypeCode.UInt64 => (ulong)value,
                TypeCode.Int64 => (ulong)(long)value,
                TypeCode.SByte => (ulong)(sbyte)value,
                TypeCode.Byte => (byte)value,
                TypeCode.Int16 => (ulong)(short)value,
                TypeCode.UInt16 => (ushort)value,
                _ => throw new InvalidOperationException()
            };
        }

        public override void WriteAsPropertyName(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            WriteAsPropertyNameCore(writer, value, options);
        }

        private unsafe void WriteAsPropertyNameCore(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var key = ConvertToUInt64(value);

            if (_nameCacheWrite.TryGetValue(key, out var formatted))
            {
                writer.WriteStringValue(formatted);
                return;
            }

            var original = value.ToString();

            if (IsValidIdentifier(original))
            {
                if (options.DictionaryKeyPolicy != null)
                {
                    original = FormatJsonName(original, options.DictionaryKeyPolicy);
                    writer.WritePropertyName(original);
                    return;
                }

                original = FormatJsonName(original, _namingPolicy);

                if (_nameCacheWrite.Count < NameCacheSizeSoftLimit)
                {
                    formatted = JsonEncodedText.Encode(original, options.Encoder);
                    writer.WritePropertyName(formatted);
                    _nameCacheWrite.TryAdd(key, formatted);
                }
                else
                {
                    writer.WritePropertyName(original);
                }
            }
        }

        public override T ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ReadAsPropertyNameCore(ref reader, typeToConvert, options);
        }

        private unsafe T ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var success = TryParseEnumCore(ref reader, options, out var value);

            if (!success)
            {
                throw new JsonException();
            }

            return value;
        }

        private static bool IsValidIdentifier(string value)
        {
            return (value[0] >= 'A' && (!s_isEnumSigned || !value.StartsWith(NumberFormatInfo.CurrentInfo.NegativeSign)));
        }

        private static bool TryParseEnumCore(ref Utf8JsonReader reader, JsonSerializerOptions options, out T value)
        {
            char[]? buffer = null;
            var bufferLength = reader.HasValueSequence ? checked((int)reader.ValueSequence.Length) : reader.ValueSpan.Length;
            var charBuffer = bufferLength <= 128 
                ? stackalloc char[128]
                : (buffer = ArrayPool<char>.Shared.Rent(bufferLength));

            var charsWritten = reader.CopyString(charBuffer);
            ReadOnlySpan<char> source = charBuffer[..charsWritten];

            var success = Enum.TryParse(source, out T result) || Enum.TryParse(source, true, out result);

            if (buffer is not null)
            {
                charBuffer[..charsWritten].Clear();
                ArrayPool<char>.Shared.Return(buffer);
            }

            value = result;
            return success;

        }

        private T ReadEnumUsingNamingPolicy(string? enumString)
        {
            if (_namingPolicy is null)
            {
                throw new JsonException();
            }

            if (enumString is null)
            {
                throw new JsonException();
            }

            Debug.Assert(_nameCacheRead is not null, "Enum cache should be initialized when reading with naming policy");

            bool success;
            if (!(success = _nameCacheRead.TryGetValue(enumString, out var value)) && enumString.Contains(ValueSeparator))
            {
                var enumValues = enumString.Split(ValueSeparator);
                var result = 0UL;

                for (var i = 0; i < enumValues.Length; i++)
                {
                    success = _nameCacheRead.TryGetValue(enumValues[i], out value);
                    if (!success)
                    {
                        break;
                    }

                    result |= ConvertToUInt64(value);
                }

                value = (T)Enum.ToObject(typeof(T), result);

                if (success && _nameCacheRead.Count < NameCacheSizeSoftLimit)
                {
                    _nameCacheRead[enumString] = value;
                }
            }

            if (!success)
            {
                throw new JsonException();
            }

            return value;
        }

        private static string FormatJsonName(string value, JsonNamingPolicy? namingPolicy)
        {
            if (namingPolicy is null)
            {
                return value;
            }

            if (!value.Contains(ValueSeparator))
            {
                return namingPolicy.ConvertName(value);
            }
            else
            {
                var enumValues = value.Split(ValueSeparator);
                for (var i = 0; i < enumValues.Length; i++)
                {
                    var name = namingPolicy.ConvertName(enumValues[i]);
                    if (name is null)
                    {
                        throw new InvalidOperationException();
                    }
                    enumValues[i] = name;
                }

                return string.Join(ValueSeparator, enumValues);
            }
        }
    }
}
