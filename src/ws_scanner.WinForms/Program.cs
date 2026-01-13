using Microsoft.Extensions.DependencyInjection;
using ws_scanner.WinForms;

ApplicationConfiguration.Initialize();

var services = new ServiceCollection();

//services.AddSingleton<IScannerService, ScannerService>();
services.AddSingleton<MainForm>();

var provider = services.BuildServiceProvider();

Application.Run(provider.GetRequiredService<MainForm>());
