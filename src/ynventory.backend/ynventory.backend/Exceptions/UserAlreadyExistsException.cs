using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class UserAlreadyExistsException : YnventoryException
    {
        public UserAlreadyExistsException(string userName) : base(ErrorCodes.User.UserAlreadyExists, "UserAlreadyExists", userName) 
        { 
            UserName = userName;
        }

        public string UserName { get; set; }

        public override IDictionary<string, object?>? Data => new Dictionary<string, object?>()
        {
            ["userName"] = UserName
        };
    }
}
