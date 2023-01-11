using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsDemo.File
{
    public interface IProcessFileService
    {
        Task ProcessFile(Stream stream);
    }
}
