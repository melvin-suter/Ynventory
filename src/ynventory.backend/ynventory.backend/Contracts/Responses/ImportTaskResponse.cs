using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Responses
{
    public class ImportTaskResponse
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public ImportTaskState TaskState { get; set; }
        public ImportTaskType TaskType { get; set; }
        public ImportErrorResponse[] Errors {get;set;}
        public DateTime createdAt {get;set;}
        public DateTime finishedAt {get;set;}
    }
}
