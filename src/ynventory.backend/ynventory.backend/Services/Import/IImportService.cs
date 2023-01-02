using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Contracts.Requests;

namespace Ynventory.Backend.Services.Import
{
    public interface IImportService
    {
        public Task<IEnumerable<ImportTaskResponse>> getTasks();
        public Task<ImportTaskResponse> CreateTask(CreateImportTaskRequest request);
    }
}
