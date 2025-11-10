using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.UI;
using iKiosk.UI.Services;
using iKiosk.UI.Services.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

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

			app.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((context, config) =>
				{
					// 🔹 Add appsettings.json manually (ensures WPF project reads it)
					config.SetBasePath(Directory.GetCurrentDirectory());
					config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
				})
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.AddConsole();
				})
				.ConfigureServices((context, services) =>
				{
					// 🔹 Get Base URL from appsettings.json
					var baseUrl = context.Configuration["ApiSettings:BaseUrl"];

					// API Client
					services.AddHttpClient<IApiClient, ApiClient>(client =>
					{
						client.BaseAddress = new Uri(baseUrl);
						client.Timeout = TimeSpan.FromSeconds(15);
					});

					// WPF components
					services.AddSingleton<App>();
					services.AddSingleton<MainWindow>();

					// ViewModels
					services.AddScoped<MainViewModel>();

					// Services
					services.AddScoped<IMessageService, MessageService>();
					services.AddSingleton<IViewNavigation, ViewNavigation>();
				});
	}
}

