namespace Ynventory.Data.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public virtual ICollection<CardMetadata> Cards { get; set; } = null!;
    }
}
