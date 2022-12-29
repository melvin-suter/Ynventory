using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class UserNotFoundException : YnventoryException
    {
        private const string UserNameNotFound = "UserNameNotFound";
        private const string UserIdNotFound = "UserIdNotFound";

        public UserNotFoundException(string userName) : base(ErrorCodes.User.UserNotFound, UserNameNotFound, userName)
        {
            UserName = userName;
        }

        public UserNotFoundException(int userId) : base(ErrorCodes.User.UserNotFound, UserIdNotFound, userId)
        {
            UserId = userId;
        }

        public string? UserName { get; set; }
        public int UserId { get; set; } = -1;

        public override IDictionary<string, object?>? Data
        {
            get
            {
                var result = new Dictionary<string, object?>();
                if (UserName is not null)
                {
                    result["userName"] = UserName;
                }

                if (UserId >= 0)
                {
                    result["userId"] = UserId;
                }

                return result;
            }
        }
    }
}
