using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Responses
{
    public class CollectionFolderCardResponse
    {
        public int Id { get; set; }
        public Guid CardMetadataId { get; set; }
        public int Quantity { get; set; }
        public CardFinish CardFinish { get; set; }
    }
}
