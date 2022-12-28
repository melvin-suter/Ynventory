using Microsoft.CSharp.RuntimeBinder;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ynventory.Backend.ServiceImplementations.Data
{
    public class ScryfallClient : IScryfallClient, IDisposable
    {
        private readonly HttpClient _client = new()
        {
            BaseAddress = new Uri("https://api.scryfall.com")
        };

        public async Task<CardMetadataResponse> GetMetadata(Guid cardMetadataId)
        {
            var response = await _client.GetAsync($"/cards/{cardMetadataId}");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ScryfallCardNotFoundException(cardMetadataId);
                }

                throw new ScryfallApiException($"Scryfall API call failed with status code: {response.StatusCode}"); //ToDo more verbose error info
            }

            var serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(new ScryfallMetadataConverter());

            var metadata = await response.Content.ReadFromJsonAsync<CardMetadataResponse>(serializerOptions);
            if (metadata is null)
            {
                throw new ScryfallApiException("Failed to deserialize card metadata from scryfall api");
            }

            return metadata;
            
            //var responseString = await response.Content.ReadAsStringAsync();
            //if (responseString is null)
            //{
            //    throw new ScryfallApiException("Response was empty");
            //}

            //dynamic responseObject = JObject.Parse(responseString);

            //var metadata = new CardMetadataResponse
            //{
            //    Id = responseObject.id,
            //    Name = responseObject.name,
            //    Lang = responseObject.lang,
            //    Layout = responseObject.layout,
            //    ImageUrlSmall = responseObject.image_uris.small,
            //    ImageUrlLarge = responseObject.image_uris.large,
            //    ManaCostTotal = responseObject.cmc,
            //    ColorIdentity = responseObject.color_identity,
            //    Keywords = responseObject.keywords,
            //    Type = responseObject.type_line,

            //};

            //try
            //{
            //    var face = responseObject.card_faces[0];
            //    metadata.ManaCost = face.mana_cost;
            //    metadata.Colors = face.colors;
            //    metadata.OracleText = GetOptional(() => face.oracle_text);
            //    metadata.Toughness = GetOptional(() => int.TryParse(face.toughness, out int t) ? t : 0);
            //    metadata.Power = GetOptional(() => int.TryParse(face.power, out int p) ? p : 0);
            //}
            //catch (RuntimeBinderException)
            //{
            //    metadata.ManaCost = responseObject.mana_cost;
            //    metadata.Colors = responseObject.colors;
            //    metadata.OracleText = GetOptional(() => responseObject.oracle_text);
            //    metadata.Toughness = GetOptional(() => int.TryParse(responseObject.toughness, out int t) ? t : 0);
            //    metadata.Power = GetOptional(() => int.TryParse(responseObject.power, out int p) ? p : 0);
            //}

            //return metadata;
        }

        private static T GetOptional<T>(Func<T> accessor)
        {
            try
            {
                return accessor();
            }
            catch (RuntimeBinderException)
            {
            }
            return default!;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _client.Dispose();
        }
    }
}
