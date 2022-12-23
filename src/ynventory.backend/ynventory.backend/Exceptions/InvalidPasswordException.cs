using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base(ResourcesReader.GetErrorMessage("InvalidPassword"))
        { 
        }
    }
}
