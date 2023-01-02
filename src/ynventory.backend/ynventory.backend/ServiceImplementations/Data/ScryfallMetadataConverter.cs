using System.Text.Json;
using System.Text.Json.Serialization;
using Ynventory.Backend.Contracts.Responses;

namespace Ynventory.Backend.ServiceImplementations.Data
{
    public class ScryfallMetadataConverter : JsonConverter<CardMetadataResponse>
    {
        private class CardFace
        {
            public string ManaCost { get; set; } = null!;
            public string Type { get; set; } = null!;
            public string? OracleText { get; set; }
            public int? Power { get; set; }
            public int? Toughness { get; set; }
            public string[]? Colors { get; set; }
        }

        public override CardMetadataResponse? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var result = new CardMetadataResponse();
            List<CardFace>? faces = null;
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            var startDepth = reader.CurrentDepth;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject && reader.CurrentDepth == startDepth)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName && reader.CurrentDepth == 1)
                {
                    switch (reader.GetString())
                    {
                        case "id":
                            reader.Read();
                            result.Id = Guid.Parse(reader.GetString()!);
                            break;
                        case "name":
                            reader.Read();
                            result.Name = reader.GetString()!;
                            break;
                        case "lang":
                            reader.Read();
                            result.Lang = reader.GetString();
                            break;
                        case "layout":
                            reader.Read();
                            result.Layout = reader.GetString();
                            break;
                        case "image_uris":
                            reader.Read();
                            if (reader.TokenType != JsonTokenType.StartObject)
                            {
                                throw new JsonException();
                            }
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonTokenType.EndObject)
                                {
                                    break;
                                }

                                if (reader.TokenType == JsonTokenType.PropertyName)
                                {
                                    switch (reader.GetString())
                                    {
                                        case "small":
                                            reader.Read();
                                            result.ImageUrlSmall = reader.GetString()!;
                                            break;
                                        case "large":
                                            reader.Read();
                                            result.ImageUrlLarge = reader.GetString()!;
                                            break;
                                    }
                                }
                            }
                            break;
                        case "type_line":
                            reader.Read();
                            result.Type = reader.GetString();
                            break;
                        case "mana_cost":
                            reader.Read();
                            result.ManaCost = reader.GetString();
                            break;
                        case "oracle_text":
                            reader.Read();
                            result.OracleText = reader.GetString();
                            break;
                        case "power":
                            reader.Read();
                            result.Power = int.TryParse(reader.GetString(), out var power) ? power : null;
                            break;
                        case "toughness":
                            reader.Read();
                            result.Toughness = int.TryParse(reader.GetString(), out var toughness) ? toughness : null;
                            break;
                        case "cmc":
                            reader.Read();
                            result.ManaCostTotal = (int)reader.GetDouble();
                            break;
                        case "colors":
                            reader.Read();
                            result.Colors = ReadStringArray(ref reader);
                            break;
                        case "color_identity":
                            reader.Read();
                            result.ColorIdentity = ReadStringArray(ref reader);
                            break;
                        case "keywords":
                            reader.Read();
                            result.Keywords = ReadStringArray(ref reader);
                            break;
                        case "legalities":
                            reader.Read();
                            result.Legalities = ReadObjectAsDictionary(ref reader);
                            break;
                        case "card_faces":
                            reader.Read();
                            if (reader.TokenType != JsonTokenType.StartArray)
                            {
                                throw new JsonException();
                            }
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonTokenType.EndArray)
                                {
                                    break;
                                }

                                if (reader.TokenType != JsonTokenType.StartObject)
                                {
                                    throw new JsonException();
                                }

                                var face = new CardFace();
                                while (reader.Read())
                                {
                                    if (reader.TokenType == JsonTokenType.EndObject)
                                    {
                                        break;
                                    }

                                    if (reader.TokenType == JsonTokenType.PropertyName)
                                    {
                                        switch (reader.GetString())
                                        {
                                            case "mana_cost":
                                                reader.Read();
                                                face.ManaCost = reader.GetString()!;
                                                break;
                                            case "type_line":
                                                reader.Read();
                                                face.Type = reader.GetString()!;
                                                break;
                                            case "oracle_text":
                                                reader.Read();
                                                face.OracleText = reader.GetString();
                                                break;
                                            case "power":
                                                reader.Read();
                                                face.Power = int.TryParse(reader.GetString(), out var p) ? p : null;
                                                break;
                                            case "toughness":
                                                reader.Read();
                                                face.Toughness = int.TryParse(reader.GetString(), out var t) ? t : null;
                                                break;
                                            case "colors":
                                                reader.Read();
                                                face.Colors = ReadStringArray(ref reader);
                                                break;
                                        }
                                    }
                                }
                                (faces ??= new List<CardFace>()).Add(face);
                            }
                            break;
                    }
                }
            }

            if (faces is not null && faces.Count > 0)
            {
                var face = faces[0];
                result.ManaCost ??= face.ManaCost;
                result.Type ??= face.Type;
                result.OracleText ??= face.OracleText;
                result.Power ??= face.Power;
                result.Toughness ??= face.Toughness;
            }

            return result;
        }

        private static string[] ReadStringArray(ref Utf8JsonReader reader)
        {
            var resultBuilder = new List<string>();
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.String)
                {
                    throw new JsonException();
                }

                var item = reader.GetString()!;
                resultBuilder.Add(item);
            }
            return resultBuilder.ToArray();
        }


        private static Dictionary<string, string> ReadObjectAsDictionary(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var result = new Dictionary<string, string>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var key = reader.GetString()!;
                    reader.Read();
                    if (reader.TokenType != JsonTokenType.String)
                    {
                        throw new JsonException();
                    }
                    var value = reader.GetString()!;
                    result[key] = value;
                }
                else
                {
                    throw new JsonException();
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, CardMetadataResponse value, JsonSerializerOptions options)
        {
            throw new NotSupportedException("Writing is not supported with this converter");
        }
    }
}
