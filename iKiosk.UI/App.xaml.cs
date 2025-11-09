using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel;
using System.Windows;

namespace iKiosk.UI
{
	public partial class App : Application
	{
		public IHost? Host { get; set; }
		private IServiceProvider? _serviceProvider;

		public App()
		{
			
		}

		public App(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			Resources.MergedDictionaries.Add(
			new ResourceDictionary
			{
				Source = new Uri("pack://application:,,,/iKiosk.UI;component/Styles/ApplicationThemes/Default/Theme.xaml",
								 UriKind.Absolute)
			});
			Resources.MergedDictionaries.Add(
			new ResourceDictionary
			{
				Source = new Uri("pack://application:,,,/iKiosk.UI;component/Themes/Generic.xaml",
								 UriKind.Absolute)
			});
			Resources.MergedDictionaries.Add(
			new ResourceDictionary
			{
				// Default Language is English
				Source = new Uri("pack://application:,,,/iKiosk.UI;component/Resources/StringResources.en.xaml",
								 UriKind.Absolute)
			});


			// Initialize DI
			var services = new ServiceCollection();
			ConfigureServices(services);

			_serviceProvider = services.BuildServiceProvider();

			// Resolve MainWindow with its ViewModel (and dependencies)
			//var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
			//mainWindow.Show();
			var mainWindow = Host!.Services.GetRequiredService<MainWindow>();
			mainWindow.DataContext = Host.Services.GetRequiredService<MainViewModel>();
			mainWindow.Show();
		}

		private void ConfigureServices(IServiceCollection services)
		{
			// Register all your services here
			services.AddSingleton<IMessageService, MessageService>();
			services.AddSingleton<IViewNavigation, ViewNavigation>();

			// ✅ Register ViewModels
			services.AddSingleton<MainViewModel>();

			// ✅ Register Views
			services.AddSingleton<MainWindow>();
		}
	}
}


