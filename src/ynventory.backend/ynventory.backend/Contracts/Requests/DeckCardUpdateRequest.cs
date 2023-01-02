using System.ComponentModel.DataAnnotations;

namespace Ynventory.Backend.Contracts.Requests
{
    public class DeckCardUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid CardMetadataId { get; set; }
    }
}
