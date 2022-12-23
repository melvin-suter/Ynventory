namespace Ynventory.Backend.Contracts.Requests
{
    public class UserCreateRequest
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
