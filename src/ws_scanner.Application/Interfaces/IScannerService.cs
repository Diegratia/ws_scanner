using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws_scanner.Application.Interfaces
{
    public interface IScannerService
    {
        Task StartAsync(string port);
        Task StopAsync();
    }
}
