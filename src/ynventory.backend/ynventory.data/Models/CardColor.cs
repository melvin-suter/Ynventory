namespace Ynventory.Data.Models
{
    public class CardColor
    {
        public int Id { get; set; }
        public string Color { get; set; } = null!;
        public Guid CardMetadataId { get; set; }
        public virtual CardMetadata Metadata { get; set; } = null!;
    }
}
