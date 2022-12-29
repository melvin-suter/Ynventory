namespace Ynventory.Backend.Exceptions
{
    public class CardNotFoundException : EntityNotFoundException
    {
        public CardNotFoundException(int cardId) : base("Card", cardId)
        {
        }
    }
}
