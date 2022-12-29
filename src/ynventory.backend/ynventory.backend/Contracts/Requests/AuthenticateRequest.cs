using System.ComponentModel.DataAnnotations;

namespace Ynventory.Backend.Contracts.Requests
{
    public class AuthenticateRequest
    {
        [Required] public string UserName { get; set; } = null!;
        [Required] public string Password { get; set; } = null!;
    }
}
