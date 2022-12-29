using Ynventory.Backend.Contracts.Responses;

namespace Ynventory.Backend.Services.Data
{
    public interface ICardMetadataService
    {
        public Task<CardMetadataResponse> GetCardMetadata(Guid cardMetadataId);
        public Task<CardMetadataResponse> UpdateCardMetadata(Guid cardMetadataId);
    }
}
