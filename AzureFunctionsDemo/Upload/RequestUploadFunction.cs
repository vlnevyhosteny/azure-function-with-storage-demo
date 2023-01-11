using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace AzureFunctionsDemo.Upload
{
    public class RequestUploadFunction
    {
        private readonly ILogger<RequestUploadFunction> _logger;
        private readonly IUploadService _uploadService;

        public RequestUploadFunction(ILogger<RequestUploadFunction> logger, IUploadService uploadService)
        {
            _logger = logger;
            _uploadService = uploadService;
        }

        [FunctionName("RequestUploadFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "user" })]
        [OpenApiParameter(name: "user", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Identification of user that uploads file")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(RequestUploadFunctionResponse), Description = "Returns requested upload url")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string user = req.Query["user"];

            try
            {
                string uploadUrl = await this._uploadService.GetUploadUrl(user);

                return new OkObjectResult(new RequestUploadFunctionResponse
                {
                    UploadUrl = uploadUrl,
                });
            } 
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to get upload url");

                return new InternalServerErrorResult();
            }
        }
    }
}

