namespace Ynventory.Backend.Services.Import
{
    public interface IImportService
    {
        public Task ImportCSV(string csv);
    }
}
