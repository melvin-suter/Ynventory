using System.ComponentModel.DataAnnotations;
using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Requests
{
    public class CreateImportTaskRequest
    {
        [Required] public string FileName { get; set; }
        [Required] public ImportTaskType TaskType { get; set; }
        [Required] public byte[] FileData { get; set; }
        [Required] public int CollectionId {get;set;}
        [Required] public int CollectionItemId {get;set;}
    }
}
