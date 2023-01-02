using System.ComponentModel.DataAnnotations;

namespace Ynventory.Backend.Contracts.Requests
{
    public class DeckCardCreateRequest
    {
        [Required]
        public Guid CardMetadataId { get; set; }
    }
}
