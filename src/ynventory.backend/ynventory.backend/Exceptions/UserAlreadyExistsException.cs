using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() : base(ResourcesReader.GetErrorMessage("UserAlreadyExists")) { }
    }
}
