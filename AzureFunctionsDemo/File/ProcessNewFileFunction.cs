using AzureFunctionsDemo.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsDemo.File
{
    public class ProcessNewFileFunction
    {
        private readonly IProcessFileService _processFileService;

        public ProcessNewFileFunction(IProcessFileService processFileService)
        {
            _processFileService = processFileService;
        }

        [FunctionName("ProcessNewFileFunction")]
        public async Task Run([BlobTrigger("azurefunctionsdemo/{name}")] Stream blob)
        {
            await _processFileService.ProcessFile(blob);
        }
    }
}
