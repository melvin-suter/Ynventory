using System.ComponentModel.DataAnnotations;

namespace Ynventory.Backend.Contracts.Requests
{
    public class CollectionFolderCreateRequest
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
