namespace Ynventory.Backend.Contracts.Requests
{
    public class UserChangePasswordRequest
    {
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!; 
    }
}
