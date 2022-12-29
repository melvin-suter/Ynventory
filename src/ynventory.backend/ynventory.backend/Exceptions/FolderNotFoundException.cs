namespace Ynventory.Backend.Exceptions
{
    public class FolderNotFoundException : EntityNotFoundException
    {
        public FolderNotFoundException(int folderId) : base("Folder", folderId) 
        { 
        }
    }
}
