namespace Ynventory.Backend.Services.Authentication
{
    public interface IAuthenticateService
    {
        public Task Authenticate(string username, string password);
    }
}
