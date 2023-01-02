using System.ComponentModel.DataAnnotations;
using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Requests
{
    public class CollectionItemCreateRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public CollectionItemType Type { get; set; }

        public string? Description { get; set; }
        public string? Notes { get; set; }
    }
}
