namespace Ynventory.Data.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public virtual ICollection<Folder> Folders { get; set; } = null!;
    }
}
