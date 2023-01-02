namespace Ynventory.Backend.Exceptions
{
    public class DeckCardNotFoundException : EntityNotFoundException
    {
        public DeckCardNotFoundException(int cardId) : base("DeckCard", cardId)
        {
        }
    }
}
