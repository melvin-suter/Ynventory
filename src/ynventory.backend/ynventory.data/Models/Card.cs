using Ynventory.Data.Enums;

namespace Ynventory.Data.Models
{
    public class Card
    {
        public int Id { get; set; }
        public Guid MetadataId { get; set; }
        public virtual CardMetadata Metadata { get; set; } = null!;
        public int ParentItemId { get; set; }
        public int Quantity { get; set; }
        public CardFinish Finish { get; set; }
        public bool IsCommander { get; set; }
        public virtual CollectionItem ParentItem { get; set; } = null!;
    }
}
