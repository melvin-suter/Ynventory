using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;

namespace Ynventory.Backend.Services.Identity
{
    public interface IUserService
    {
        public Task<UserResponse> CreateUser(UserCreateRequest request);
        public Task<UserResponse> GetUser(string userName);
        public Task<UserResponse> GetUser(int userId);
        public Task ChangePassword(int userId, UserChangePasswordRequest request);
    }
}
