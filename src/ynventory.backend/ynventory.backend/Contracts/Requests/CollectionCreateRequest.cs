using System.ComponentModel.DataAnnotations;

namespace Ynventory.Backend.Contracts.Requests
{
    public class CollectionCreateRequest
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
