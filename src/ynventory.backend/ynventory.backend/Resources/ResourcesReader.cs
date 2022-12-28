using System.Globalization;
using System.Resources;

namespace Ynventory.Backend.Resources
{
    public static class ResourcesReader
    {
        private const string RESOURCE_PATH = "Resources/";
        private static readonly string ErrorMessagesResources = typeof(ErrorMessages).FullName!;

        public static string GetErrorMessage(string key, params object?[] args)
        {
            return GetErrorMessage(key, CultureInfo.InvariantCulture, args);
        }

        public static string GetErrorMessage(string key, CultureInfo culture, params object?[] args)
        {
            var resourceKey = $"Error_{key}";
            return ReadMessage(ErrorMessagesResources, resourceKey, culture, args);
        }

        private static string ReadMessage(string file, string key, CultureInfo culture, params object?[] args)
        {
            var manager = new ResourceManager(file, typeof(ErrorMessages).Assembly);

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
