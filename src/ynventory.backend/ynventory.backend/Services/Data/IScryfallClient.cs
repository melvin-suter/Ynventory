using Ynventory.Backend.Contracts.Responses;

namespace Ynventory.Backend.Services.Data
{
    public interface IScryfallClient
    {
        public Task<CardMetadataResponse> GetMetadata(Guid cardMetadataId);
    }
}
