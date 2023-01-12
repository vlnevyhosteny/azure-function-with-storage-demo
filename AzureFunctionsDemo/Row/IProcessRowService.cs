using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsDemo.Row
{
    public interface IProcessRowService
    {
        Task ProcessRow(string message);
    }
}
