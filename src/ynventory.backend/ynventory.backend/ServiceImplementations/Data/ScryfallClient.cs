using System.Text.Json;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Data;

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
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _client.Dispose();
        }
    }
}
