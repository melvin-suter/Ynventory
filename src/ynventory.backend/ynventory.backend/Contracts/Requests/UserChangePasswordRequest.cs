using System.ComponentModel.DataAnnotations;

namespace Ynventory.Backend.Contracts.Requests
{
    public class UserChangePasswordRequest
    {
        [Required] public string OldPassword { get; set; } = null!;
        [Required] public string NewPassword { get; set; } = null!; 
    }
}
