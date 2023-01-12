using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AzureFunctionsDemo.Row
{
    public class ProcessRowFunction
    {
        private readonly IProcessRowService _processRowService;

        public ProcessRowFunction(IProcessRowService processRowService)
        {
            _processRowService = processRowService;
        }

        [FunctionName("ProcessRowFunction")]
        public async Task Run([QueueTrigger(Constants.FILE_ROWS_QUEUE_NAME)] string message)
        {
            await _processRowService.ProcessRow(message);
        }
    }
}
