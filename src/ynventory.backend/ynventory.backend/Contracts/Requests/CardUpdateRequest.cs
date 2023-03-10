using System.ComponentModel.DataAnnotations;
using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Requests
{
    public class CardUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid CardMetadataId { get; set; }
        public int Quantity { get; set; }
        public CardFinish CardFinish { get; set; }
        public bool IsCommander { get; set; }

    }
}
