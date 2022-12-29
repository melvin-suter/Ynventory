using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Responses
{
    public class CollectionFolderCardResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public CardFinish CardFinish { get; set; }
        public CardMetadataResponse Metadata { get; set; } = null!;
    }
}
