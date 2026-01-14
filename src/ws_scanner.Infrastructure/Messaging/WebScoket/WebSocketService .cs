using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using ws_scanner.Application.Interfaces;

namespace ws_scanner.Infrastructure.Messaging.WebScoket
{
    public class WebSocketService : IWebSocketService
    {
        private WebSocket? _client;
        public event Func<string, Task>? OnDocumentTypeReceived;

        //public async Task HandleAsync(WebSocket socket)
        //{
        //    _client = socket;
        //    var buffer = new byte[1024];

        //    while (socket.State == WebSocketState.Open)
        //    {
        //        var result = await socket.ReceiveAsync(buffer, CancellationToken.None);
        //        var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);

        //        var payload = JsonSerializer.Deserialize<DocMsg>(msg);
        //        if (!string.IsNullOrEmpty(payload?.Type))
        //        {
        //            if (OnDocumentTypeReceived != null)
        //                await OnDocumentTypeReceived(payload.Type);
        //        }
        //    }
        //}

        public async Task HandleAsync(WebSocket socket)
        {
            _client = socket;
            var buffer = new byte[1024];

            while (socket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result;

                try
                {
                    result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                }
                catch
                {
                    break; // socket closed
                }

                var msg = Encoding.UTF8.GetString(buffer, 0, result.Count).Trim();

                Debug.WriteLine($"📨 WS RAW MESSAGE: {msg}");

                // 🔥 SIMPLE & TAHAN ERROR
                string? type = null;

                // 1️⃣ JSON
                if (msg.StartsWith("{"))
                {
                    try
                    {
                        var payload = JsonSerializer.Deserialize<DocMsg>(msg);
                        type = payload?.Type;
                    }
                    catch
                    {
                        Debug.WriteLine("❌ INVALID JSON, IGNORED");
                    }
                }
                // 2️⃣ STRING BIASA
                else
                {
                    if (msg.Equals("ktp", StringComparison.OrdinalIgnoreCase))
                        type = "ktp";
                    else if (msg.Equals("passport", StringComparison.OrdinalIgnoreCase))
                        type = "passport";
                }

                if (!string.IsNullOrEmpty(type))
                {
                    Debug.WriteLine($"📩 DOC TYPE RECEIVED: {type}");

                    if (OnDocumentTypeReceived != null)
                        await OnDocumentTypeReceived(type);

                    // OPTIONAL: ACK ke client
                    await SendAsync($"READY:{type}");
                }
                else
                {
                    Debug.WriteLine("⚠️ MESSAGE IGNORED");
                }
            }
        }


        public async Task SendAsync(string message)
        {
            if (_client?.State == WebSocketState.Open)
            {
                var data = Encoding.UTF8.GetBytes(message);

                await _client.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
        

        private record DocMsg(string Type);
    }
}
