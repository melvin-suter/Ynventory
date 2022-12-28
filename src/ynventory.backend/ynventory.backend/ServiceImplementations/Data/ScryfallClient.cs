using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Data;

namespace Ynventory.Backend.ServiceImplementations.Data
{
    public class ScryfallClient : IScryfallClient, IDisposable
    {
        private readonly HttpClient _client = new()
        {
            BaseAddress = new Uri("https://api.scryfall.com/cards")
        };

        public async Task<CardMetadataResponse> GetMetadata(Guid cardMetadataId)
        {
            var response = await _client.GetAsync($"/{cardMetadataId}");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ScryfallCardNotFoundException(cardMetadataId);
                }

                throw new ScryfallApiException($"Scryfall API call failed with status code: {response.StatusCode}"); //ToDo more verbose error info
            }
            
            var responseString = await response.Content.ReadAsStringAsync();
            if (responseString is null)
            {
                throw new ScryfallApiException("Response was empty");
            }

            dynamic responseObject = JObject.Parse(responseString);

            var metadata = new CardMetadataResponse
            {
                Id = responseObject.id,
                Name = responseObject.name,
                Lang = responseObject.lang,
                Layout = responseObject.layout,
                ImageUrlSmall = responseObject.imageUris.small,
                ImageUrlLarge = responseObject.imageUris.large,
                ManaCostTotal = responseObject.cmc,
                ColorIdentity = responseObject.color_identity,
                Keywords = responseObject.keywords,
                Type = responseObject.type_line,

            };

            try
            {
                var face = responseObject.card_faces[0];
                metadata.ManaCost = face.mana_cost;
                metadata.Colors = face.colors;
                metadata.OracleText = GetOptional(() => face.oracle_text);
                metadata.Toughness = GetOptional(() => int.TryParse(face.toughness, out int t) ? t : 0);
                metadata.Power = GetOptional(() => int.TryParse(face.power, out int p) ? p : 0);
            }
            catch (RuntimeBinderException)
            {
                metadata.ManaCost = responseObject.mana_cost;
                metadata.Colors = responseObject.colors;
                metadata.OracleText = GetOptional(() => responseObject.oracle_text);
                metadata.Toughness = GetOptional(() => int.TryParse(responseObject.toughness, out int t) ? t : 0);
                metadata.Power = GetOptional(() => int.TryParse(responseObject.power, out int p) ? p : 0);
            }

            return metadata;
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
