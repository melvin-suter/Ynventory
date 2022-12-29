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

        public Task<CollectionFolderResponse> CreateFolder(int collectionId, CollectionFolderCreateRequest request);
        public Task<IEnumerable<CollectionFolderResponse>> GetFolders(int collectionId);
        public Task<CollectionFolderResponse> GetFolder(int collectionId, int folderId);
        public Task<CollectionFolderResponse> UpdateFolder(int collectionId, CollectionFolderUpdateRequest request);
        public Task DeleteFolder(int collectionId, int folderId);

        public Task<CollectionFolderCardResponse> CreateCard(int collectionId, int folderId, CollectionFolderCardCreateRequest request);
        public Task<IEnumerable<CollectionFolderCardResponse>> GetCards(int collectionId, int folderId); 
        public Task<CollectionFolderCardResponse> GetCard(int collectionId, int folderId, int cardId);
        public Task<CollectionFolderCardResponse> UpdateCard(int collectionId, int folderId, CollectionFolderCardUpdateRequest request);
        public Task DeleteCard(int collectionId, int folderId, int cardId);
    }
}
