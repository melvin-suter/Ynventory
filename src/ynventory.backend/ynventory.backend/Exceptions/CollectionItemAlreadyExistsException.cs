namespace Ynventory.Backend.Exceptions
{
    public class CollectionItemAlreadyExistsException : EntityAlreadyExistsException
    {
        public CollectionItemAlreadyExistsException(string name) : base("CollectionItem", name)
        {
        }
    }
}
