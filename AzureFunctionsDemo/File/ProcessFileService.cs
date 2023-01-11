using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace AzureFunctionsDemo.File
{
    public class ProcessFileService : IProcessFileService
    {
        private readonly QueueServiceClient _queueServiceClient;
        private readonly ILogger<ProcessFileService> _logger;

        public ProcessFileService(QueueServiceClient queueServiceClient, ILogger<ProcessFileService> logger) 
        {
            _queueServiceClient = queueServiceClient;
            _logger = logger;
        }

        public async Task ProcessFile(Stream stream)
        {
            using var reader = new StreamReader(stream);
            reader.ReadLine(); // Just stupid way to skip header line 

            string queueName = Constants.FILE_ROWS_QUEUE_NAME;

            QueueClient queueClient = _queueServiceClient.GetQueueClient(queueName);
            await queueClient.CreateIfNotExistsAsync();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                try
                {
                    FileRow fileRow = ParseFileRow(line);

                    await queueClient.SendMessageAsync(JsonSerializer.Serialize(fileRow));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to parse file row");
                }
            }
        }

        private static FileRow ParseFileRow(string row)
        {
            var tokens = row.Split(',');

            return new()
            {
                Id = Int32.Parse(tokens[0]),
                FirstName = tokens[1],
                LastName = tokens[2],
                Email = tokens[3],
                Profession = tokens[5],
            };
        }
    }
}
