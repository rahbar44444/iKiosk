using iKiosk.Framework.Wpf.Interface;
using iKiosk.UI.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iKiosk.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
	{
		private readonly IViewNavigation _navigation;
		public MainWindow(MainViewModel vm, IViewNavigation navigation)
		{
			InitializeComponent();
			mainWindow.Loaded += Window_Loaded;
			DataContext = vm;
			_navigation = navigation;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.InvokeAsync(() =>
			{
				_navigation.NavigateTo<HomeViewModel>();
			});

		}
	}
}