namespace Ynventory.Backend.Contracts.Responses
{
    public class DeckCardResponse
    {
        public int Id { get; set; }
        public CardMetadataResponse Metadata { get; set; } = null!;
    }
}
