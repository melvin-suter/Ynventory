using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Contracts.Requests;

namespace Ynventory.Backend.Services.Import
{
    public interface IImportService
    {
        public Task<IEnumerable<ImportTaskResponse>> GetTasks();
        public Task<ImportTaskResponse> CreateTask(CreateImportTaskRequest request);
    }
}
