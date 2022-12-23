using Microsoft.EntityFrameworkCore;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Authentication;
using Ynventory.Backend.Services.Identity;
using Ynventory.Data;
using Ynventory.Data.Models;

namespace Ynventory.Backend.ServiceImplementations.Identity
{
    public class UserService : IUserService
    {
        private readonly YnventoryDbContext _context;

        public UserService(YnventoryDbContext context)
        {
            _context = context;
        }

        public async Task ChangePassword(int userId, UserChangePasswordRequest request)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }

            //Validate old password
            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))
            {
                throw new InvalidPasswordException();
            }

            //Set new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            await _context.SaveChangesAsync();
        }

        public async Task<UserResponse> CreateUser(UserCreateRequest request)
        {
            if (await _context.Users.AnyAsync(x => x.Email.Equals(request.UserName, StringComparison.Ordinal)))
            {
                throw new UserAlreadyExistsException();
            }

            var user = new User
            {
                Email = request.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return ToResponse(user);
        }

        public async Task<UserResponse> GetUser(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(userName, StringComparison.Ordinal));
            if (user is null)
            {
                throw new UserNotFoundException(userName);
            }
            return ToResponse(user);
        }

        public async Task<UserResponse> GetUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }
            return ToResponse(user);
        }

        private static UserResponse ToResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                UserName = user.Email
            };
        }
    }
}
