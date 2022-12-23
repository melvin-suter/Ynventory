namespace Ynventory.Data.Models
{
    public class CardColorIdentity
    {
        public int Id { get; set; }
        public string ColorIdentity { get; set; } = null!;
        public Guid CardMetadataId { get; set; }
        public virtual CardMetadata Metadata { get; set; } = null!;
    }
}
