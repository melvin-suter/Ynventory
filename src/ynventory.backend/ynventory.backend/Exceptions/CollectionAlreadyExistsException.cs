namespace Ynventory.Backend.Exceptions
{
    public class CollectionAlreadyExistsException : EntityAlreadyExistsException
    {
        public CollectionAlreadyExistsException(string name) : base("Collection", name)
        {
        }
    }
}
