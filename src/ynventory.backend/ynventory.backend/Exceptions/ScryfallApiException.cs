using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class ScryfallApiException : YnventoryException
    {
        public ScryfallApiException(string errorDetail) : base(ErrorCodes.Scryfall.ApiError, "Scryfall_ApiError", errorDetail)
        {
        }
    }
}
