namespace Ynventory.Data.Models
{
    public class CardKeyword
    {
        public int Id { get; set; }
        public string Keyword { get; set; } = null!;
        public Guid CardMetadataId { get; set; }
        public virtual CardMetadata Metadata { get; set; } = null!;
    }
}
