using Microsoft.EntityFrameworkCore;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Data;
using Ynventory.Data;
using Ynventory.Data.Models;

namespace Ynventory.Backend.ServiceImplementations.Data
{
    public class DeckService : IDeckService
    {
        private readonly YnventoryDbContext _context;
        private readonly ICardMetadataService _cardMetadataService;

        public DeckService(YnventoryDbContext context, ICardMetadataService cardMetadataService)
        {
            _context = context;
            _cardMetadataService = cardMetadataService;
        }

        public async Task<DeckResponse> CreateDeck(DeckCreateRequest request)
        {
            if (await _context.Decks.AnyAsync(x => x.Name == request.Name))
            {
                throw new DeckAlreadyExistsException(request.Name);
            }

            var deck = new Deck
            {
                Name = request.Name,
                Description = request.Description,
            };

            _context.Decks.Add(deck);
            await _context.SaveChangesAsync();
            return ToResponse(deck);
        }

        public async Task<IEnumerable<DeckResponse>> GetDecks()
        {
            return (await _context.Decks.ToArrayAsync()).Select(ToResponse).ToArray();
        }

        public async Task<DeckResponse> GetDeck(int deckId)
        {
            var deck = await _context.Decks.FindAsync(deckId);
            if (deck is null)
            {
                throw new DeckNotFoundException(deckId);
            }

            return ToResponse(deck);
        }

        public async Task<DeckResponse> UpdateDeck(DeckUpdateRequest request)
        {
            var deck = await _context.Decks.FindAsync(request.Id);
            if (deck is null)
            {
                throw new DeckNotFoundException(request.Id);
            }

            if (deck.Name != request.Name)
            {
                if (await _context.Decks.AnyAsync(x => x.Name == request.Name))
                {
                    throw new DeckAlreadyExistsException(request.Name);
                }
            }

            deck.Name = request.Name;
            deck.Description = request.Description;

            await _context.SaveChangesAsync();
            return ToResponse(deck);
        }

        public async Task DeleteDeck(int deckId)
        {
            var deck = await _context.Decks.FindAsync(deckId);
            if (deck is null)
            {
                throw new DeckNotFoundException(deckId);
            }

            _context.Decks.Remove(deck);
            await _context.SaveChangesAsync();
        }

        public async Task<DeckCardResponse> CreateCard(int deckId, DeckCardCreateRequest request)
        {
            var deck = await _context.Decks.FindAsync(deckId);
            if (deck is null)
            {
                throw new DeckNotFoundException(deckId);
            }

            await _cardMetadataService.GetCardMetadata(request.CardMetadataId);

            var deckCard = new DeckCard
            {
                Deck = deck,
                DeckId = deckId,
                MetadataId = request.CardMetadataId
            };

            _context.DeckCards.Add(deckCard);

            await _context.SaveChangesAsync();
            return ToResponse(deckCard);

            
        }

        public async Task<IEnumerable<DeckCardResponse>> GetCards(int deckId)
        {
            return (await _context.DeckCards.Where(x => x.DeckId == deckId).ToArrayAsync()).Select(ToResponse).ToArray();
        }

        public async Task<DeckCardResponse> GetCard(int deckId, int cardId)
        {
            //ToDo redo relations
            if (!await _context.Decks.AnyAsync(x => x.Id == deckId))
            {
                throw new DeckNotFoundException(deckId);
            }

            var card = await _context.DeckCards.FindAsync(cardId);
            if (card is null)
            {
                throw new DeckCardNotFoundException(cardId);
            }

            return ToResponse(card);
        }

        public async Task<DeckCardResponse> UpdateCard(int deckId, DeckCardUpdateRequest request)
        {
            //ToDo redo relations
            if (!await _context.Decks.AnyAsync(x => x.Id == deckId))
            {
                throw new DeckNotFoundException(deckId);
            }

            var card = await _context.DeckCards.FindAsync(request.Id);
            if (card is null)
            {
                throw new DeckCardNotFoundException(request.Id);
            }

            if (card.MetadataId != request.CardMetadataId)
            {
                await _cardMetadataService.GetCardMetadata(request.CardMetadataId);
            }
            else
            {
                await _cardMetadataService.UpdateCardMetadata(request.CardMetadataId);
            }

            card.MetadataId = request.CardMetadataId;

            await _context.SaveChangesAsync();
            return ToResponse(card);
        }

        public async Task DeleteCard(int deckId, int cardId)
        {
            //ToDo redo relations
            if (!await _context.Decks.AnyAsync(x => x.Id == deckId))
            {
                throw new DeckNotFoundException(deckId);
            }

            var card = await _context.DeckCards.FindAsync(cardId);
            if (card is null)
            {
                throw new DeckCardNotFoundException(cardId);
            }

            _context.DeckCards.Remove(card);
            await _context.SaveChangesAsync();
        }

        private static DeckResponse ToResponse(Deck deck)
        {
            return new DeckResponse
            {
                Id = deck.Id,
                Name = deck.Name,
                Description = deck.Description,
            };
        }

        private static DeckCardResponse ToResponse(DeckCard deckCard)
        {
            return new DeckCardResponse
            {
                Id = deckCard.Id,
                Metadata = deckCard.Metadata.ToResponse()
            };
        }
    }
}
