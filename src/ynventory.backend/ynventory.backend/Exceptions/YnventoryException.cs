using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class YnventoryException : Exception
    {
        public int Code { get; set; }

        public YnventoryException(int code, string messageKey, params object?[] values) : base(FormatMessage(messageKey, values))
        {
            Code = code;
        }
         
        private static string FormatMessage(string key, params object?[] values)
        {
            return ResourcesReader.ErrorMessages.GetString(key, values);
        }

        public virtual new IDictionary<string, object?>? Data { get; }
    }
}
