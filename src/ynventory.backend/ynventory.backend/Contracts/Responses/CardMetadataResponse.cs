namespace Ynventory.Backend.Contracts.Responses
{
    public class CardMetadataResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Lang { get; set; }
        public string? Layout { get; set; }
        public string ImageUrlSmall { get; set; } = null!;
        public string ImageUrlLarge { get; set; } = null!;
        public string? Type { get; set; }
        public string? ManaCost { get; set; }
        public string? OracleText { get; set; }
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public int? ManaCostTotal { get; set; }
        public string[] Colors { get; set; } = null!;
        public string[] ColorIdentity { get; set; } = null!;
        public string[] Keywords { get; set; } = null!;
    }
}
