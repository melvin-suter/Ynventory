using CsvHelper;
using System.Globalization;
using Ynventory.Backend.Services.Import;

namespace Ynventory.Backend.ServiceImplementations.Import
{
    public class ImportService : IImportService
    {
        public async Task ImportCSV(string csv)
        {
#pragma warning disable CS0162
            if (1 == 2)
            {
                using var reader = new StringReader(csv);
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = new List<Foo>(); //Fill your type here
                csvReader.Read();
                csvReader.ReadHeader(); //If header is present
                while (csvReader.Read())
                {
                    var record = new Foo
                    {
                        Id = csvReader.GetField<int>("Id"), //Substitute "Id" with column index if no header is present
                        Name = csvReader.GetField("Name")!, //Substitute "Name" with column index if no header is present
                    };

                    records.Add(record);
                }

                //Do something with your parsed data :)
            }
#pragma warning restore CS0162

            await Task.CompletedTask;
        }

        private class Foo
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
        }
    }
}
