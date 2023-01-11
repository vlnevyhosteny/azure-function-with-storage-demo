using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using AzureFunctionsDemo.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsDemo.Upload
{
    public class UploadService : IUploadService
    {
        private readonly IBlobStorageService _blobStorageService;

        public UploadService(IBlobStorageService blobStorageService) 
        { 
            _blobStorageService = blobStorageService;
        }

        public async Task<string> GetUploadUrl(string userIdentification)
        {
            return await _blobStorageService.GetUploadUrlWithSaS(BlobStorageService.CONTAINER_NAME, this.composeFileName(userIdentification));
        }

        private string composeFileName(string userIdentification) => $"{userIdentification}-${Guid.NewGuid()}.csv";
    }
}
