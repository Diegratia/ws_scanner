using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws_scanner.Application.Interfaces
{
    public interface IOcrApiClient
    {
        Task<string> SendAsync(string imagePath, string type);
    }

}
