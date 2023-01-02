using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Responses
{
    public class ImportErrorResponse
    {
        public int Id { get; set; }
        public string Error { get; set; }
        public string ErrorData { get; set; }
    }
}
