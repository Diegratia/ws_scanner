using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Logging;
using ws_scanner.Application.Dtos;
using ws_scanner.Application.Interfaces;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;

namespace ws_scanner.Infrastructure.Messaging.WebScoket
{
    public class WebSocketService : IWebSocketService
    {
        private WebSocket? _client;
        public WsRequest? _wsRequest { get; private set; }
        private readonly ILogger<WebSocketService> _logger;

        // event ke Application layer
        public event Func<WsRequest, Task>? OnRequestReceived;
        public WebSocketService(
            ILogger<WebSocketService> logger
        )
        {
            _logger = logger;
        }


        public async Task HandleAsync(WebSocket socket)
        {
            _client = socket;
            var buffer = new byte[4096];


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
                _logger.LogDebug("[WS-IN ] RAW={Raw}", msg);

                if (!msg.StartsWith("{"))
                {
                    _logger.LogWarning("[WS-IN ] NON JSON MESSAGE IGNORED");
                    continue;
                }

                WsRequest? req;

                try
                {
                    req = JsonSerializer.Deserialize<WsRequest>(msg);
                    // hilangkan log ini ntar
                    _logger.LogDebug(
                    $"[WS-IN ] PARSED cmd={req.Cmd}, doc={req.DocType}, action={req.ActionType}, source={req.ActionSource}"
                );
                }
                catch (Exception ex)
                {
                    _logger.LogError($"[WS-IN ] INVALID WS JSON: {ex.Message}");
                    _logger.LogWarning($"[WS-IN ]NON-JSON MESSAGE IGNORED");
                    continue;
                }

                if (req == null)
                {
                    Debug.WriteLine("⚠️ NULL REQUEST IGNORED");
                    continue;
                }

                // simpan context
                _wsRequest = req;

                _logger.LogDebug(
                    $"📩 WS REQUEST: cmd={req.Cmd}, doc={req.DocType}, action={req.ActionType}, source={req.ActionSource}"
                );

                // kirim ke Application layer
                if (OnRequestReceived != null)
                    await OnRequestReceived(req);
            }
        }

        public async Task SendAsync(string message)
        {
            _logger.LogDebug($"[WS-OUT] RAW={message}");
            if (_client?.State == WebSocketState.Open)
            {
                var data = Encoding.UTF8.GetBytes(message);
                await _client.SendAsync(
                    data,
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );
            }
        }
    }
}
