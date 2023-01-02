using System.ComponentModel.DataAnnotations;
using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Requests
{
    public class CardCreateRequest 
    {
        [Required]
        public Guid CardMetadataId { get; set; }

        public int Quantity { get; set; }
        public CardFinish CardFinish { get; set; }
        public bool IsCommander { get; set; }
    }
}
