using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ws_scanner.Application.Interfaces;
using ws_scanner.Application.Services;
using ws_scanner.Infrastructure;
using ws_scanner.Infrastructure.Api;
using ws_scanner.Infrastructure.Messaging.WebScoket;
using ws_scanner.WinForms.Forms;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;

ApplicationConfiguration.Initialize();


var services = new ServiceCollection();

services.AddLogging(builder =>
{
    builder
        .SetMinimumLevel(LogLevel.Debug)
        .AddDebug();      // Output window
        //.AddConsole(); // kalau mau console
});

services.AddSingleton<IImageWatcher, ImageWatcher>();
services.AddSingleton<IOcrApiClient, OcrApiClient>();
services.AddSingleton<IWebSocketService, WebSocketService>();
services.AddSingleton<WebSocketServer>();

services.AddSingleton<IImagePipelineService, ImagePipelineService>();

services.AddSingleton<WsServerForm>();

var provider = services.BuildServiceProvider();
Application.Run(provider.GetRequiredService<WsServerForm>());
