using Microsoft.Extensions.DependencyInjection;
using ws_scanner.WinForms.Forms;

ApplicationConfiguration.Initialize();

var services = new ServiceCollection();

//services.AddSingleton<IScannerService, ScannerService>();
services.AddSingleton<TcpServerForm>();

var provider = services.BuildServiceProvider();

Application.Run(provider.GetRequiredService<TcpServerForm>());
