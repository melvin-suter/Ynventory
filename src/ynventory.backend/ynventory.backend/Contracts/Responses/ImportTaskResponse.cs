﻿using Ynventory.Data.Enums;

namespace Ynventory.Backend.Contracts.Responses
{
    public class ImportTaskResponse
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public ImportTaskType TaskType { get; set; }
        public ImportErrorResponse[] Errors {get;set;}
    }
}
