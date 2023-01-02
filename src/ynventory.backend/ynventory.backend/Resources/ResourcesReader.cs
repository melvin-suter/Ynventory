using System.Globalization;
using System.Resources;

namespace Ynventory.Backend.Resources
{
    public class ResourcesReader
    {
        public static readonly ResourcesReader ErrorMessages = new(typeof(ErrorMessages), "Error");
        public static readonly ResourcesReader Strings = new(typeof(Strings));
        
        private readonly Type _resourceType;
        private readonly string? _resourceKeyPrefix;

        private ResourcesReader(Type resourceType, string? resourceKeyPrefix = null)
        {
            _resourceType = resourceType;
            _resourceKeyPrefix = resourceKeyPrefix;
        }

        public string GetString(string key, params object?[] args)
        {
            return GetString(key, CultureInfo.InvariantCulture, args);
        }

        public string GetString(string key, CultureInfo culture, params object?[] args)
        {
            var resourceKey = _resourceKeyPrefix != null ? $"{_resourceKeyPrefix}_{key}" : key;
            return ReadString(_resourceType, resourceKey, culture, args);
        }

        private static string ReadString(Type resourceType, string key, CultureInfo culture, params object?[] args)
        {
            var manager = new ResourceManager(resourceType.FullName!, resourceType.Assembly);

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
