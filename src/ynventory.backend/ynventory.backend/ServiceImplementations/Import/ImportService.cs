using CsvHelper;
using System.Globalization;
using Ynventory.Backend.Services.Import;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Data;
using Microsoft.EntityFrameworkCore;
using Ynventory.Data.Models;
using Ynventory.Backend.Services.Data;
using Ynventory.Data.Enums;

namespace Ynventory.Backend.ServiceImplementations.Import
{
    public class ImportService : IImportService
    {
        private readonly YnventoryDbContext _context;
        private readonly ICollectionService _collectionService;

        public ImportService(YnventoryDbContext context, ICollectionService collectionService) {
            _context = context;
            _collectionService = collectionService;
        }

        public async Task<IEnumerable<ImportTaskResponse>> getTasks()
        {
            var tasks = await _context.ImportTasks.ToArrayAsync();
            return tasks.Select(ToResponse).ToArray();
        }

        public async Task<ImportTaskResponse> CreateTask(CreateImportTaskRequest request)
        {
            var task = new ImportTask
            {
                FileName = request.FileName,
                FileData = request.FileData,
                TaskType = request.TaskType,
                TaskState = Ynventory.Data.Enums.ImportTaskState.Pending
            };

            _context.ImportTasks.Add(task);

            await _context.SaveChangesAsync();

            switch(task.TaskType){
                case ImportTaskType.DelverCSV:
                    Import_DelverCSV(task);
                    break;
            }

            return ToResponse(task);
        }






        private static ImportTaskResponse ToResponse(ImportTask task)
        {
            return new ImportTaskResponse
            {
                Id = task.Id,
                FileName = task.FileName,
                TaskType = task.TaskType,
                Errors = task.ImportErrors.Select(ToResponse).ToArray()
            };
        }

        private static ImportErrorResponse ToResponse(ImportError taskError)
        {
            return new ImportErrorResponse
            {
                Id = taskError.Id,
                Error = taskError.Error
            };
        }



        private async Task Import_DelverCSV(ImportTask task){
            using var reader = new StringReader(System.Text.Encoding.Default.GetString(task.FileData));
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            var result = true;
            var records = new List<Card>();
            csvReader.Read();
            csvReader.ReadHeader();
            while (csvReader.Read())
            {
                try {
                    await _collectionService.CreateCard(task.CollectionId, task.CollectionItemId, new CardCreateRequest(){
                        CardMetadataId = csvReader.GetField<Guid>("Scryfall ID"),
                        Quantity = csvReader.GetField<int>("Quantity"),
                        CardFinish = csvReader.GetField<CardFinish>("Foil"),
                        IsCommander = false
                    });
                } catch(Exception ex){
                    string errorData = "";
                    string value;
                    for(int i=0; csvReader.TryGetField<string>(i, out value); i++) {
                        errorData += value + ", ";
                    }
                    _context.ImportErrors.Add(new ImportError(){
                        Error = ex.Message,
                        ErrorData = errorData,
                        ImportTaskId = task.Id
                    });
                    result = false;
                }
            }

            task.TaskState = result ? ImportTaskState.Successfull : ImportTaskState.Failed;
            
            await _context.SaveChangesAsync();

            await Task.CompletedTask;
        }

    }
}
