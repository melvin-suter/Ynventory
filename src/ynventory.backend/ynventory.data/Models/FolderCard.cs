using Ynventory.Data.Enums;

namespace Ynventory.Data.Models
{
    public class FolderCard
    {
        public int Id { get; set; }
        public Guid CardMetadataId { get; set; }
        public virtual CardMetadata Metadata { get; set; } = null!;
        public int Quantity { get; set; }
        public int FolderId { get; set; }
        public virtual Folder Folder { get; set; } = null!;
        public CardFinish Finish { get; set; }

        public virtual ICollection<Deck> Decks { get; set; } = null!;

    }
}
