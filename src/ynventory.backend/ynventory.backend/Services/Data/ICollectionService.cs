using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;

namespace Ynventory.Backend.Services.Data
{
    public interface ICollectionService
    {
        public Task<CollectionResponse> CreateCollection(CollectionCreateRequest request);
        public Task<IEnumerable<CollectionResponse>> GetCollections();
        public Task<CollectionResponse> GetCollection(int collectionId);
        public Task<CollectionResponse> UpdateCollection(CollectionUpdateRequest request);
        public Task DeleteCollection(int collectionId);
        public Task<IEnumerable<CardResponse>> GetCards(int collectionId);

        public Task<CollectionItemResponse> CreateItem(int collectionId, CollectionItemCreateRequest request);
        public Task<IEnumerable<CollectionItemResponse>> GetItems(int collectionId);
        public Task<CollectionItemResponse> GetItem(int collectionId, int collectionItemId);
        public Task<CollectionItemResponse> UpdateItem(int collectionId, CollectionItemUpdateRequest request);
        public Task DeleteItem(int collectionId, int collectionItemId);

        public Task<CardResponse> CreateCard(int collectionId, int collectionItemId, CardCreateRequest request);
        public Task<IEnumerable<CardResponse>> GetCards(int collectionId, int collectionItemId); 
        public Task<CardResponse> GetCard(int collectionId, int collectionItemId, int cardId);
        public Task<CardResponse> UpdateCard(int collectionId, int collectionItemId, CardUpdateRequest request);
        public Task DeleteCard(int collectionId, int collectionItemId, int cardId);
        public Task<CardResponse> MoveCard(int collectionId, int collectionItemId, int cardId, MoveCardRequest request);
    }
}
