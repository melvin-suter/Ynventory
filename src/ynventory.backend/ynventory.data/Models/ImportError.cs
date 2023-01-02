using Ynventory.Data.Enums;

namespace Ynventory.Data.Models
{
    public class ImportError
    {
        public int Id { get; set; }
        public string ErrorData {get;set;}
        public string Error {get;set;}
        public int ImportTaskId { get; set; }
        public virtual ImportTask ImportTask { get; set; } = null!;
        
    }
}
