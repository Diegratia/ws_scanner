using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ws_scanner.Application.Interfaces;

namespace ws_scanner.Infrastructure.Messaging.WebScoket
{
    public class WebSocketServer
    {
        private IHost? _host;
        private readonly IWebSocketService _wsService;

        public WebSocketServer(IWebSocketService wsService)
        {
            _wsService = wsService;
        }

        public async Task StartAsync(int port)
        {
            if (_host != null) return;

            _host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel()
                        .UseUrls($"http://0.0.0.0:{port}")
                        .Configure(app =>
                        {
                            app.UseWebSockets();

                            app.Map("/ws", wsApp =>
                            {
                                wsApp.Run(async context =>
                                {
                                    if (!context.WebSockets.IsWebSocketRequest)
                                    {
                                        context.Response.StatusCode = 400;
                                        return;
                                    }

                                    var socket = await context.WebSockets.AcceptWebSocketAsync();
                                    await _wsService.HandleAsync(socket);
                                });
                            });

                            // OPTIONAL: default response
                            app.Run(ctx =>
                            {
                                ctx.Response.StatusCode = 404;
                                return Task.CompletedTask;
                            });
                        });
                })
                .Build();

            await _host.StartAsync();
        }

        public async void Stop()
        {
            if (_host != null)
            {
                await _host.StopAsync();
                _host = null;
            }
        }
    }
}

