using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.UI;
using iKiosk.UI.Services;
using iKiosk.UI.Services.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace iKiosk.Startup
{
	public class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			// Resolve App from DI
			var app = host.Services.GetRequiredService<App>();

			// Pass host instance to App (this line now works)
			app.Host = host;
			
			//app.InitializeComponent();
			app.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.AddConsole();
				})
				.ConfigureServices((context, services) =>
				{
					// API Client
					services.AddHttpClient<IApiClient, ApiClient>(client =>
					{
						client.BaseAddress = new Uri("https://localhost:7015");
						client.Timeout = TimeSpan.FromSeconds(15);
					});

					// WPF components
					services.AddSingleton<App>();
					services.AddSingleton<MainWindow>();

					// ViewModels
					services.AddTransient<MainViewModel>();

					// Services
					services.AddScoped<IMessageService, MessageService>();
					services.AddSingleton<IViewNavigation, ViewNavigation>();
				});
	}
}
