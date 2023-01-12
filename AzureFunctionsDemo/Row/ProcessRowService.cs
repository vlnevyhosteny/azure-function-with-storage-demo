using Azure.Data.Tables;
using AzureFunctionsDemo.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureFunctionsDemo.Row
{
    public class ProcessRowService : IProcessRowService
    {
        private readonly TableServiceClient _tableServiceClient;

        public ProcessRowService(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

        public async Task ProcessRow(string message)
        {
            FileRowMessage fileRow = JsonSerializer.Deserialize<FileRowMessage>(message);

            await _tableServiceClient.CreateTableIfNotExistsAsync(Constants.PEOPLE_TABLE_NAME);

            TableClient tableClient = _tableServiceClient.GetTableClient(Constants.PEOPLE_TABLE_NAME);

            await tableClient.UpsertEntityAsync(new RowEntity(fileRow));
        }
    }
}
