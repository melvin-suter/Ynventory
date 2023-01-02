using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Responses
{
    public class CardResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public CardFinish CardFinish { get; set; }
        public bool IsCommander { get; set; }
        public CardMetadataResponse Metadata { get; set; } = null!;
    }
}
