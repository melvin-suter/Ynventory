namespace Ynventory.Backend.Exceptions
{
    public class CollectionItemNotFoundException : EntityNotFoundException
    {
        public CollectionItemNotFoundException(int collectionItemId) : base("CollectionItem", collectionItemId) 
        { 
        }
    }
}
