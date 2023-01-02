using Ynventory.Data.Enums;

namespace Ynventory.Data.Models
{
    public class ImportTask
    {
        public int Id { get; set; }
        public string FileName {get;set;}
        public ImportTaskState TaskState {get;set;}
        public ImportTaskType TaskType {get;set;}
        public Byte[] FileData {get;set;}
        public virtual ICollection<ImportError> ImportErrors { get; set; } = null!;
        public DateTime createdAt {get;set;}
        public DateTime finishedAt {get;set;}

        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; } = null!;
        public int CollectionItemId { get; set; }
        public virtual CollectionItem CollectionItem { get; set; } = null!;
        

    }
}
