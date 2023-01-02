using Microsoft.EntityFrameworkCore;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Data;
using Ynventory.Data;
using Ynventory.Data.Enums;
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

        public async Task<CollectionResponse> CreateCollection(CollectionCreateRequest request)
        {
            var exists = await _context.Collections.AnyAsync(x => x.Name == request.Name);
            if (exists)
            {
                throw new CollectionAlreadyExistsException(request.Name);
            }

            var collection = new Collection
            {
                Name = request.Name,
                Description = request.Description,
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

        public async Task<CollectionResponse> UpdateCollection(CollectionUpdateRequest request)
        {
            var collection = await _context.Collections.FindAsync(request.Id);
            if (collection is null)
            {
                throw new CollectionNotFoundException(request.Id);
            }

            if (request.Name != request.Name)
            {
                var exists = await _context.Collections.AnyAsync(x => x.Name == request.Name);
                if (exists)
                {
                    throw new CollectionAlreadyExistsException(request.Name);
                }
            }

            collection.Name = request.Name;
            collection.Description = request.Description;

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

        public async Task<IEnumerable<CardResponse>> GetCards(int collectionId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var cards = collection.Items.Where(x => x.Type == CollectionItemType.Folder).SelectMany(x => x.Cards).Select(ToResponse).ToArray();

            return cards;
        }

        public async Task<CollectionItemResponse> CreateItem(int collectionId, CollectionItemCreateRequest request)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var exists = collection.Items.Any(x => x.Name == request.Name && 
                                                   x.Type == request.Type);
            if (exists)
            {
                throw new CollectionItemAlreadyExistsException(request.Name);
            }

            var collectionItem = new CollectionItem
            {
                Name = request.Name,
                Type = request.Type,
                Description = request.Description,
                Notes = request.Notes,
            };

            collection.Items.Add(collectionItem);

            await _context.SaveChangesAsync();

            return ToResponse(collectionItem);
        }

        public async Task<IEnumerable<CollectionItemResponse>> GetItems(int collectionId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            return collection.Items.Select(ToResponse).ToArray();
        }

        public async Task<CollectionItemResponse> GetItem(int collectionId, int collectionItemId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var collectionItem = collection.Items.FirstOrDefault(x => x.Id == collectionItemId);
            if (collectionItem is null)
            {
                throw new CollectionItemNotFoundException(collectionItemId);
            }

            return ToResponse(collectionItem);
        }

        public async Task<CollectionItemResponse> UpdateItem(int collectionId, CollectionItemUpdateRequest request)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var collectionItem = collection.Items.FirstOrDefault(x => x.Id == request.Id);
            if (collectionItem is null)
            {
                throw new CollectionItemNotFoundException(request.Id);
            }

            if (collectionItem.Name != request.Name)
            {
                var exists = collection.Items.Any(x => x.Name == request.Name &&
                                                       x.Type == collectionItem.Type);
                if (exists)
                {
                    throw new CollectionItemAlreadyExistsException(request.Name);
                }
            }

            collectionItem.Name = request.Name;
            collectionItem.Description = request.Description;
            collectionItem.Notes = request.Notes;

            await _context.SaveChangesAsync();

            return ToResponse(collectionItem);
        }

        public async Task DeleteItem(int collectionId, int collectionItemId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var collectionItem = collection.Items.FirstOrDefault(x => x.Id == collectionItemId);
            if (collectionItem is null)
            {
                throw new CollectionItemNotFoundException(collectionItemId);
            }

            collection.Items.Remove(collectionItem);

            await _context.SaveChangesAsync();
        }

        public async Task<CardResponse> CreateCard(int collectionId, int collectionItemId, CardCreateRequest request)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var collectionItem = collection.Items.FirstOrDefault(x => x.Id == collectionItemId);
            if (collectionItem is null)
            {
                throw new CollectionItemNotFoundException(collectionItemId);
            }

            //check if card exists (this will throw in case it doesn't find anything)
            _ = await _cardMetadataService.GetCardMetadata(request.CardMetadataId);

            var card = new Card
            {
                MetadataId = request.CardMetadataId,
                Finish = request.CardFinish,
                Quantity = request.Quantity,
                IsCommander = request.IsCommander,
            };

            collectionItem.Cards.Add(card);

            await _context.SaveChangesAsync();

            return ToResponse(card);
        }

        public async Task<IEnumerable<CardResponse>> GetCards(int collectionId, int collectionItemId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var collectionItem = collection.Items.FirstOrDefault(x => x.Id == collectionItemId);
            if (collectionItem is null)
            {
                throw new CollectionItemNotFoundException(collectionItemId);
            }

            return collectionItem.Cards.Select(ToResponse);
        }

        public async Task<CardResponse> GetCard(int collectionId, int collectionItemId, int cardId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var collectionItem = collection.Items.FirstOrDefault(x => x.Id == collectionItemId);
            if (collectionItem is null)
            {
                throw new CollectionItemNotFoundException(collectionItemId);
            }

            var card = collectionItem.Cards.FirstOrDefault(x => x.Id == cardId);
            if (card is null)
            {
                throw new CardNotFoundException(cardId);
            }

            return ToResponse(card);
        }

        public async Task<CardResponse> UpdateCard(int collectionId, int collectionItemId, CardUpdateRequest request)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var collectionItem = collection.Items.FirstOrDefault(x => x.Id == collectionItemId);
            if (collectionItem is null)
            {
                throw new CollectionItemNotFoundException(collectionItemId);
            }

            var card = collectionItem.Cards.FirstOrDefault(x => x.Id == request.Id);
            if (card is null)
            {
                throw new CardNotFoundException(request.Id);
            }

            //check if card exists (this will throw in case it doesn't find anything)
            await _cardMetadataService.GetCardMetadata(request.CardMetadataId);

            //If it's the same metadataId, update the card metadata on the database
            if (request.CardMetadataId == card.MetadataId)
            {
                await _cardMetadataService.UpdateCardMetadata(request.CardMetadataId);
            }

            card.MetadataId = request.CardMetadataId;
            card.Finish = request.CardFinish;
            card.Quantity = request.Quantity;
            card.IsCommander = request.IsCommander;

            await _context.SaveChangesAsync();

            return ToResponse(card);
        }

        public async Task DeleteCard(int collectionId, int collectionItemId, int cardId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(collectionId);
            }

            var collectionItem = collection.Items.FirstOrDefault(x => x.Id == collectionItemId);
            if (collectionItem is null)
            {
                throw new CollectionItemNotFoundException(collectionItemId);
            }

            var card = collectionItem.Cards.FirstOrDefault(x => x.Id == cardId);
            if (card is null)
            {
                throw new CardNotFoundException(cardId);
            }

            collectionItem.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }

        private static CollectionResponse ToResponse(Collection collection)
        {
            return new CollectionResponse
            {
                Id = collection.Id,
                Name = collection.Name,
                Description = collection.Description,
                CardCount = collection.Items?.Sum(x => x.Cards?.Sum(x => x.Quantity) ?? 0) ?? 0
            };
        }

        private static CollectionItemResponse ToResponse(CollectionItem collectionItem)
        {
            return new CollectionItemResponse
            {
                Id = collectionItem.Id,
                Name = collectionItem.Name,
                Type = collectionItem.Type,
                Description = collectionItem.Description,
                Notes = collectionItem.Notes,
                CardCount = collectionItem.Cards?.Sum(x => x.Quantity) ?? 0,
            };
        }

        private static CardResponse ToResponse(Card card)
        {
            return new CardResponse
            {
                Id = card.Id,
                CardFinish = card.Finish,
                Quantity = card.Quantity,
                IsCommander = card.IsCommander,
                Metadata = ToResponse(card.Metadata)
            };
        }

        private static CardMetadataResponse ToResponse(CardMetadata metadata)
        {
            return new CardMetadataResponse
            {
                Id = metadata.Id,
                Name = metadata.Name,
                Lang = metadata.Lang,
                Layout = metadata.Layout,
                ImageUrlSmall = metadata.ImageUrlSmall,
                ImageUrlLarge = metadata.ImageUrlLarge,
                Type = metadata.Type,
                ManaCost = metadata.ManaCost,
                OracleText = metadata.OracleText,
                Power = metadata.Power,
                Toughness = metadata.Toughness,
                ManaCostTotal = metadata.ManaCostTotal,
                Colors = metadata.Colors.Select(x => x.Color).ToArray(),
                ColorIdentity = metadata.ColorIdentity.Select(x => x.ColorIdentity).ToArray(),
                Keywords = metadata.Keywords.Select(x => x.Keyword).ToArray(),
                Legalities = metadata.Legalities.ToDictionary(x => x.PlayFormat, x => x.Legality),
            };
        }
    }
}
