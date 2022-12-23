using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class UserNotFoundException : Exception
    {
        private const string USER_NAME_NOT_FOUND = "UserNameNotFound";
        private const string USER_ID_NOT_FOUND = "UserIdNotFound";

        public UserNotFoundException(string userName) : base(FormatMessage(userName))
        {
            UserName = userName;
        }

        public UserNotFoundException(int userId) : base(FormatMessage(userId))
        {
            UserId = userId;
        }

        public string? UserName { get; set; }
        public int UserId { get; set; } = -1;

        private static string FormatMessage(string userName)
        {
            return ResourcesReader.GetErrorMessage(USER_NAME_NOT_FOUND, userName);
        }

        private static string FormatMessage(int userId)
        {
            return ResourcesReader.GetErrorMessage(USER_ID_NOT_FOUND, userId);
        }
    }
}
