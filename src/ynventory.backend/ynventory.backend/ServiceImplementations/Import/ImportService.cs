using Microsoft.EntityFrameworkCore;
using System.Text;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Import;
using Ynventory.Data;
using Ynventory.Data.Enums;
using Ynventory.Data.Models;

namespace Ynventory.Backend.ServiceImplementations.Import
{
    public class ImportService : IImportService
    {
        private readonly YnventoryDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public ImportService(YnventoryDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<IEnumerable<ImportTaskResponse>> GetTasks()
        {
            var tasks = await _context.ImportTasks.ToArrayAsync();
            return tasks.Select(ToResponse).ToArray();
        }

        public async Task<ImportTaskResponse> CreateTask(CreateImportTaskRequest request)
        {
            var collection = await _context.Collections.FindAsync(request.CollectionId);
            if (collection is null)
            {
                throw new CollectionNotFoundException(request.CollectionId);
            }

            var collectionItem = collection.Items.FirstOrDefault(x => x.Id == request.CollectionItemId);
            if (collectionItem is null)
            {
                throw new CollectionItemNotFoundException(request.CollectionItemId);
            }

            var task = new ImportTask
            {
                FileName = request.FileName,
                FileData = Encoding.Default.GetBytes(request.FileData),
                TaskType = request.TaskType,
                TaskState = ImportTaskState.Pending,
                createdAt = DateTime.UtcNow
            };

            collectionItem.ImportTasks.Add(task);

            await _context.SaveChangesAsync();

            var import = task.TaskType switch
            {
                ImportTaskType.DelverCSV => new DelverCSVImport(_serviceProvider),
                _ => throw new InvalidDataException(),
            };

            await import.Run(task);

            return ToResponse(task);
        }

        private static ImportTaskResponse ToResponse(ImportTask task)
        {
            return new ImportTaskResponse
            {
                Id = task.Id,
                FileName = task.FileName,
                TaskType = task.TaskType,
                Errors = (task.ImportErrors == null) ? Array.Empty<ImportErrorResponse>() : task.ImportErrors.Select(ToResponse).ToArray(),
                createdAt = task.createdAt,
                finishedAt = task.finishedAt
            };
        }

        private static ImportErrorResponse ToResponse(ImportError taskError)
        {
            return new ImportErrorResponse
            {
                Id = taskError.Id,
                Error = taskError.Error,
                ErrorData = taskError.ErrorData
            };
        }
    }
}
