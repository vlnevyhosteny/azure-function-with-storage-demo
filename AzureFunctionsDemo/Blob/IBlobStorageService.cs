using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsDemo.Blob
{
    public interface IBlobStorageService
    {
        Task<BlobContainerClient> CreateContainerIfNotExists(string containerName);

        Task<string> GetUploadUrlWithSaS(string containerName, string blobName);
    }
}
