using Ynventory.Data;
using Ynventory.Data.Models;

namespace Ynventory.Backend.Services.Import
{
    public interface IImport
    {
        public Task Run(ImportTask task);
    }
}
