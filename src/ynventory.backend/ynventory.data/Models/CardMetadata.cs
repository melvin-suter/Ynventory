namespace Ynventory.Data.Models
{
    public class CardMetadata
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Lang { get; set; }
        public string? Layout { get; set; }
        public string ImageUrlSmall { get; set; } = null!;
        public string ImageUrlLarge { get; set; } = null!;
        public string? Type { get; set; } = null!;
        public string? ManaCost { get; set; }
        public string? OracleText { get; set; }
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public int? ManaCostTotal { get; set; }
        public virtual ICollection<CardColor> Colors { get; set; } = null!;
        public virtual ICollection<CardColorIdentity> ColorIdentity { get; set; } = null!;
        public virtual ICollection<CardKeyword> Keywords { get; set; } = null!;
        public virtual ICollection<CardLegality> Legalities { get; set; } = null!;

        public virtual ICollection<Deck> Decks { get; set; } = null!;
    }
}
