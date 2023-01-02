namespace Ynventory.Data.Models
{
    public class DeckCard
    {
        public int Id { get; set; }
        public int DeckId { get; set; }
        public virtual Deck Deck { get; set; } = null!;
        public Guid MetadataId { get; set; }
        public virtual CardMetadata Metadata { get; set; } = null!;
    }
}
