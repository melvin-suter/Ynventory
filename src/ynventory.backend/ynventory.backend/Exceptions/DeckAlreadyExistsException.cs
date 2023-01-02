namespace Ynventory.Backend.Exceptions
{
    public class DeckAlreadyExistsException : EntityAlreadyExistsException
    {
        public DeckAlreadyExistsException(string name) : base("Deck", name)
        {
        }
    }
}
