using Ynventory.Data.Enums;

namespace Ynventory.Data.Models
{
    public class CollectionItem
    {
        public int Id { get; set; }
        public CollectionItemType Type { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; } = null!;
        public virtual ICollection<Card> Cards { get; set; } = null!;
        public virtual ICollection<ImportTask> ImportTasks { get; set; } = null!;
    }
}
