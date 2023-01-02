using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Responses
{
    public class CollectionItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public CollectionItemType Type { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public int CardCount { get; set; }
    }
}
