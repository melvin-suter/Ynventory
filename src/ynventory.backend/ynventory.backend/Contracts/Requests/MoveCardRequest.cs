using System.ComponentModel.DataAnnotations;

namespace Ynventory.Backend.Contracts.Requests
{
    public class MoveCardRequest
    {
        [Required]
        public int TargetCollectionId { get; set; }
        [Required]
        public int TargetCollectionItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
