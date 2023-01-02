using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;

namespace Ynventory.Backend.Services.Data
{
    public interface IDeckService
    {
        public Task<DeckResponse> CreateDeck(DeckCreateRequest request);
        public Task<IEnumerable<DeckResponse>> GetDecks();
        public Task<DeckResponse> GetDeck(int deckId);
        public Task<DeckResponse> UpdateDeck(DeckUpdateRequest request);
        public Task DeleteDeck(int deckId);

        public Task<DeckCardResponse> CreateCard(int deckId, DeckCardCreateRequest request);
        public Task<IEnumerable<DeckCardResponse>> GetCards(int deckId);
        public Task<DeckCardResponse> GetCard(int deckId, int cardId);
        public Task<DeckCardResponse> UpdateCard(int deckId, DeckCardUpdateRequest request);
        public Task DeleteCard(int deckId, int cardId);
    }
}
