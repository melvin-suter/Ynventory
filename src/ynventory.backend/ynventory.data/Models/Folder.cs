namespace Ynventory.Data.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; } = null!;
        public virtual ICollection<FolderCard> Cards { get; set; } = null!;
    }
}
