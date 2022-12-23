using Microsoft.EntityFrameworkCore;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Authentication;
using Ynventory.Data;

namespace Ynventory.Backend.ServiceImplementations.Authentication
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly YnventoryDbContext _context;

        public AuthenticateService(YnventoryDbContext context)
        {
            _context = context;
        }

        public async Task Authenticate(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(username, StringComparison.Ordinal));
            if (user is null)
            {
                throw new UserNotFoundException(username);
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new InvalidPasswordException();
            }

        }
    }
}
