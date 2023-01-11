using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsDemo.Upload
{
    public interface IUploadService
    {
        Task<string> GetUploadUrl(string userIdentification);
    }
}
