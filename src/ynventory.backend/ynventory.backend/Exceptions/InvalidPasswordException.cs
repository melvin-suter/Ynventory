using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class InvalidPasswordException : YnventoryException
    {
        public InvalidPasswordException() : base(ErrorCodes.Authentication.InvalidPassword, "InvalidPassword")
        { 
        }
    }
}
