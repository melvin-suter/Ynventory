using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ynventory.Backend.Util
{
    public class JsonEnumConverterFactory : JsonConverterFactory
    {
        public JsonEnumConverterFactory() { }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
                typeof(JsonEnumConverter<>).MakeGenericType(typeToConvert),
                options)!;
        }
    }
}
