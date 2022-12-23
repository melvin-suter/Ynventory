using System.Globalization;
using System.Resources;

namespace Ynventory.Backend.Resources
{
    public static class ResourcesReader
    {
        private const string RESOURCE_PATH = "Resources/";
        private const string ERROR_MESSAGES_RESOURCE_FILE_NAME = "ErrorMessages.resx";

        public static string GetErrorMessage(string key, params object?[] args)
        {
            return GetErrorMessage(key, CultureInfo.InvariantCulture, args);
        }

        public static string GetErrorMessage(string key, CultureInfo culture, params object?[] args)
        {
            var resourceKey = $"Error_{key}";
            return ReadMessage(ERROR_MESSAGES_RESOURCE_FILE_NAME, resourceKey, culture, args);
        }

        private static string ReadMessage(string file, string key, CultureInfo culture, params object?[] args)
        {
            var manager = ResourceManager.CreateFileBasedResourceManager(file, RESOURCE_PATH, null);

            var value = manager.GetString(key, culture);
            if (value is null)
            {
                return "?";
            }

            try
            {
                return string.Format(value, args);
            }
            catch (FormatException)
            {
                return value;
            }
        }

    }
}
