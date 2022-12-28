namespace Ynventory.Backend.Exceptions
{
    public class FolderAlreadyExistsException : EntityAlreadyExistsException
    {
        public FolderAlreadyExistsException(string name) : base("Folder", name)
        {
        }
    }
}
