using System.ComponentModel.DataAnnotations;
using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Requests
{
    public class CreateImportTaskRequest
    {
        [Required] public string FileName { get; set; } = null!;
        [Required] public ImportTaskType TaskType { get; set; }
        [Required] public string FileData { get; set; } = null!;
        [Required] public int CollectionId {get;set;}
        [Required] public int CollectionItemId {get;set;}
    }
}
