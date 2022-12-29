using System.ComponentModel.DataAnnotations;

namespace Ynventory.Backend.Contracts.Requests
{
    public class CollectionUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
