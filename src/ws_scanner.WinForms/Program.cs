using Microsoft.Extensions.DependencyInjection;
using ws_scanner.Application.Interfaces;
using ws_scanner.Application.Services;
using ws_scanner.Infrastructure;
using ws_scanner.Infrastructure.Api;
using ws_scanner.Infrastructure.Messaging.WebScoket;
using ws_scanner.WinForms.Forms;

ApplicationConfiguration.Initialize();

var services = new ServiceCollection();



services.AddSingleton<IImageWatcher, ImageWatcher>();
services.AddSingleton<IOcrApiClient, OcrApiClient>();
services.AddSingleton<IWebSocketService, WebSocketService>();
services.AddSingleton<WebSocketServer>();

services.AddSingleton<ImagePipelineService>();

services.AddSingleton<WsServerForm>();

var provider = services.BuildServiceProvider();
Application.Run(provider.GetRequiredService<WsServerForm>());
