using AzureFunctionsDemo.Blob;
using AzureFunctionsDemo.File;
using AzureFunctionsDemo.Upload;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;

[assembly: FunctionsStartup(typeof(AzureFunctionsDemo.Startup))]
namespace AzureFunctionsDemo
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddAzureClients(clientBuilder =>
            {
                // TODO: move url to external configuration
                string azuriteConnectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
                clientBuilder.AddBlobServiceClient(azuriteConnectionString);

                clientBuilder.AddQueueServiceClient(azuriteConnectionString);
            });

            builder.Services.AddScoped<IUploadService, UploadService>();

            builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

            builder.Services.AddScoped<IProcessFileService, ProcessFileService>();
        }
    }
}
