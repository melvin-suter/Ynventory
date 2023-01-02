namespace Ynventory.Backend.Exceptions
{
    public class DeckNotFoundException : EntityNotFoundException
    {
        public DeckNotFoundException(int deckId) : base("Deck", deckId)
        {
        }
    }
}
