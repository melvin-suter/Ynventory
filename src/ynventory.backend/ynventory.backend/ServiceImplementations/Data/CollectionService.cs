using Microsoft.EntityFrameworkCore;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Data;
using Ynventory.Data;
using Ynventory.Data.Models;

namespace Ynventory.Backend.ServiceImplementations.Data
{
    public class CollectionService : ICollectionService
    {
        private readonly YnventoryDbContext _context;
        private readonly ICardMetadataService _cardMetadataService;

        public CollectionService(YnventoryDbContext context, ICardMetadataService cardMetadataService)
        {
            _context = context;
            _cardMetadataService = cardMetadataService;
        }

        public async Task<CollectionResponse> CreateCollection(CollectionCreateRequest createRequest)
        {
            var exists = await _context.Collections.AnyAsync(x => x.Name.Equals(createRequest.Name, StringComparison.Ordinal));
            if (exists)
            {
                throw new CollectionAlreadyExistsException(createRequest.Name);
            }

            var collection = new Collection
            {
                Name = createRequest.Name,
                Description = createRequest.Description,
            };

            _context.Collections.Add(collection);

            await _context.SaveChangesAsync();

            return ToResponse(collection);
        }

        public async Task<IEnumerable<CollectionResponse>> GetCollections()
        {
            var collections = await _context.Collections.ToArrayAsync();

            return collections.Select(ToResponse).ToArray(); //ToArray is called to immediatly enumerate the produced IEnumerable
        }

        public async Task<CollectionResponse> GetCollection(int collectionId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }
            return ToResponse(collection);
        }

        public async Task<CollectionResponse> UpdateCollection(CollectionUpdateRequest updateRequest)
        {
            var collection = await _context.Collections.FindAsync(updateRequest.Id);
            if (collection is null)
            {
                throw new CollectionNotFoundException(updateRequest.Id);
            }

            if (!updateRequest.Name.Equals(collection.Name, StringComparison.Ordinal))
            {
                var exists = await _context.Collections.AnyAsync(x => x.Name.Equals(updateRequest.Name, StringComparison.Ordinal));
                if (exists)
                {
                    throw new CollectionAlreadyExistsException(updateRequest.Name);
                }
            }

            collection.Name = updateRequest.Name;
            collection.Description = updateRequest.Description;

            await _context.SaveChangesAsync();

            return ToResponse(collection);

        }

        public async Task DeleteCollection(int collectionId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            _context.Collections.Remove(collection);

            await _context.SaveChangesAsync();
        }

        public async Task<CollectionFolderResponse> CreateFolder(int collectionId, CollectionFolderCreateRequest request)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var exists = collection.Folders.Any(x => x.Name.Equals(request.Name));
            if (exists)
            {
                throw new FolderAlreadyExistsException(request.Name);
            }

            var folder = new Folder
            {
                Name = request.Name,
                Description = request.Description,
            };

            collection.Folders.Add(folder);

            await _context.SaveChangesAsync();

            return ToResponse(folder);
        }

        public async Task<IEnumerable<CollectionFolderResponse>> GetFolders(int collectionId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            return collection.Folders.Select(ToResponse).ToArray();
        }

        public async Task<CollectionFolderResponse> GetFolder(int collectionId, int folderId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var folder = collection.Folders.FirstOrDefault(x => x.Id.Equals(folderId));
            if (folder is null)
            {
                throw new FolderNotFoundException(folderId);
            }

            return ToResponse(folder);
        }

        public async Task<CollectionFolderResponse> UpdateFolder(int collectionId, CollectionFolderUpdateRequest request)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var folder = collection.Folders.FirstOrDefault(x => x.Id.Equals(request.Id));
            if (folder is null)
            {
                throw new FolderNotFoundException(request.Id);
            }

            if (!folder.Name.Equals(request.Name, StringComparison.Ordinal))
            {
                var exists = collection.Folders.Any(x => x.Name.Equals(request.Name, StringComparison.Ordinal));
                if (exists)
                {
                    throw new FolderAlreadyExistsException(request.Name);
                }
            }

            folder.Name = request.Name;
            folder.Description = request.Description;

            await _context.SaveChangesAsync();

            return ToResponse(folder);
        }

        public async Task DeleteFolder(int collectionId, int folderId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var folder = collection.Folders.FirstOrDefault(x => x.Id.Equals(folderId));
            if (folder is null)
            {
                throw new FolderNotFoundException(folderId);
            }

            collection.Folders.Remove(folder);

            await _context.SaveChangesAsync();
        }

        public async Task<CollectionFolderCardResponse> CreateCard(int collectionId, int folderId, CollectionFolderCardCreateRequest request)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var folder = collection.Folders.FirstOrDefault(x => x.Id.Equals(folderId));
            if (folder is null)
            {
                throw new FolderNotFoundException(folderId);
            }

            //check if card exists (this will throw in case it doesn't find anything)
            _ = await _cardMetadataService.GetCardMetadata(request.CardMetadataId);

            var card = new FolderCard
            {
                CardMetadataId = request.CardMetadataId,
                Finish = request.CardFinish,
                Quantity = request.Quantity,
            };

            folder.Cards.Add(card);

            await _context.SaveChangesAsync();

            return ToResponse(card);
        }

        public async Task<IEnumerable<CollectionFolderCardResponse>> GetCards(int collectionId, int folderId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var folder = collection.Folders.FirstOrDefault(x => x.Id.Equals(folderId));
            if (folder is null)
            {
                throw new FolderNotFoundException(folderId);
            }

            return folder.Cards.Select(ToResponse);
        }

        public async Task<CollectionFolderCardResponse> GetCard(int collectionId, int folderId, int cardId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var folder = collection.Folders.FirstOrDefault(x => x.Id.Equals(folderId));
            if (folder is null)
            {
                throw new FolderNotFoundException(folderId);
            }

            var card = folder.Cards.FirstOrDefault(x => x.Id.Equals(cardId));
            if (card is null)
            {
                throw new CardNotFoundException(cardId);
            }

            return ToResponse(card);
        }

        public async Task<CollectionFolderCardResponse> UpdateCard(int collectionId, int folderId, CollectionFolderCardUpdateRequest request)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var folder = collection.Folders.FirstOrDefault(x => x.Id.Equals(folderId));
            if (folder is null)
            {
                throw new FolderNotFoundException(folderId);
            }

            var card = folder.Cards.FirstOrDefault(x => x.Id.Equals(request.Id));
            if (card is null)
            {
                throw new CardNotFoundException(request.Id);
            }

            //check if card exists (this will throw in case it doesn't find anything)
            await _cardMetadataService.GetCardMetadata(request.CardMetadataId);

            //If it's the same metadataId, update the card metadata on the database
            if (request.CardMetadataId.Equals(card.CardMetadataId))
            {
                await _cardMetadataService.UpdateCardMetadata(request.CardMetadataId);
            }

            card.CardMetadataId = request.CardMetadataId;
            card.Finish = request.CardFinish;
            card.Quantity = request.Quantity;

            await _context.SaveChangesAsync();

            return ToResponse(card);
        }

        public async Task DeleteCard(int collectionId, int folderId, int cardId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var folder = collection.Folders.FirstOrDefault(x => x.Id.Equals(folderId));
            if (folder is null)
            {
                throw new FolderNotFoundException(folderId);
            }

            var card = folder.Cards.FirstOrDefault(x => x.Id.Equals(cardId));
            if (card is null)
            {
                throw new CardNotFoundException(cardId);
            }

            folder.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }

        private static CollectionResponse ToResponse(Collection collection)
        {
            return new CollectionResponse
            {
                Id = collection.Id,
                Name = collection.Name,
                Description = collection.Description,
                CardCount = collection.Folders.Sum(x => x.Cards.Sum(x => x.Quantity))
            };
        }

        private static CollectionFolderResponse ToResponse(Folder folder)
        {
            return new CollectionFolderResponse
            {
                Id = folder.Id,
                Name = folder.Name,
                Description = folder.Description,
                CardCount = folder.Cards.Sum(x => x.Quantity),
            };
        }

        private static CollectionFolderCardResponse ToResponse(FolderCard card)
        {
            return new CollectionFolderCardResponse
            {
                Id = card.Id,
                CardFinish = card.Finish,
                CardMetadataId = card.CardMetadataId,
                Quantity = card.Quantity,
            };
        }
    }
}
