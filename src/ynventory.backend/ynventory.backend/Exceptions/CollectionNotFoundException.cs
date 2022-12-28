namespace Ynventory.Backend.Exceptions
{
    public class CollectionNotFoundException : EntityNotFoundException
    {
        public CollectionNotFoundException(int collectionId) : base("Collection", collectionId) 
        { 
        }
    }
}
