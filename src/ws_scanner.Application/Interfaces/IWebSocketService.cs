using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ws_scanner.Application.Interfaces
{
    public interface IWebSocketService
    {
        Task HandleAsync(System.Net.WebSockets.WebSocket socket);
        Task SendAsync(string message);

        // 🔥 EVENT
        event Func<string, Task>? OnDocumentTypeReceived;
    }

}
