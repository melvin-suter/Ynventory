﻿using CsvHelper;
using System.Globalization;
using System.Text;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Services.Data;
using Ynventory.Backend.Services.Import;
using Ynventory.Data;
using Ynventory.Data.Enums;
using Ynventory.Data.Models;

namespace Ynventory.Backend.ServiceImplementations.Import
{
    public class DelverCSVImport : IImport
    {
        private readonly IServiceProvider _provider;

        public DelverCSVImport(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }

        public async Task Run(ImportTask task)
        {
            using var scope = _provider.CreateScope();
            var collectionService = scope.ServiceProvider.GetRequiredService<ICollectionService>();
            var context = scope.ServiceProvider.GetRequiredService<YnventoryDbContext>();

            using var reader = new StringReader(Encoding.Default.GetString(task.FileData));
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            var result = true;
            var records = new List<Card>();
            csvReader.Read();
            csvReader.ReadHeader();

            while (csvReader.Read())
            {
                try
                {
                    await collectionService.CreateCard(task.CollectionItem.CollectionId, task.CollectionItemId, new CardCreateRequest
                    {
                        CardMetadataId = csvReader.GetField<Guid>("Scryfall ID"),
                        Quantity = csvReader.GetField<int>("Quantity"),
                        CardFinish = Enum.Parse<CardFinish>(csvReader.GetField("Foil")!),
                        IsCommander = false
                    });
                }
                catch (Exception ex)
                {
                    var errorDataBuilder = new StringBuilder();
                    for (var i = 0; csvReader.TryGetField<string>(i, out var value); i++)
                    {
                        //errorData += value + ", ";
                        errorDataBuilder.Append(value);
                        errorDataBuilder.Append(", ");
                    }

                    context.ImportErrors.Add(new ImportError
                    {
                        Error = ex.Message,
                        ErrorData = errorDataBuilder.ToString(),
                        ImportTaskId = task.Id,
                    });
                    result = false;
                }
            }

            task.TaskState = result ? ImportTaskState.Successfull : ImportTaskState.Failed;
            task.finishedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }
    }
}