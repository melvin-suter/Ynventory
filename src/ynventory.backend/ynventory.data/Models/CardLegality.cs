namespace Ynventory.Data.Models
{
    public class CardLegality
    {
        public int Id { get; set; }
        public string PlayFormat { get; set; } = null!;
        public string Legality { get; set; } = null!;
        public Guid CardMetadataId { get; set; }
        public virtual CardMetadata Metadata { get; set; } = null!;
    }
}
