using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using System;
using System.Threading.Tasks;

namespace AzureFunctionsDemo.Blob
{
    public class BlobStorageService : IBlobStorageService
    {
        public const string CONTAINER_NAME = "azurefunctionsdemo";

        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<BlobContainerClient> CreateContainerIfNotExists(string containerName)
        {
            BlobContainerClient container = _blobServiceClient.GetBlobContainerClient(containerName);

            bool exists = await container.ExistsAsync();
            if (!exists)
            {
                await container.CreateAsync();
            }

            return container;
        }

        public async Task<string> GetUploadUrlWithSaS(string containerName, string blobName)
        {
            BlobContainerClient blobContainerClient = await CreateContainerIfNotExists(containerName);

            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);

            BlobSasBuilder sasBuilder = new()
            {
                BlobContainerName = containerName,
                BlobName = blobName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(7),
                ContentType = "text/csv",
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read |
                                      BlobSasPermissions.Write);

            Uri sasUri = blobClient.GenerateSasUri(sasBuilder);

            return sasUri.ToString();
        }
    }
}
