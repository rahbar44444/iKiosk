using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iKiosk.UI.ViewModels
{
	public class HomeViewModel : ViewModelControlBase
	{
		private readonly IViewNavigation _navigation;
		public ICommand StartCommand { get; set; }
		public HomeViewModel(IViewNavigation navigation)
		{
			_navigation = navigation;
			StartCommand = new Command(Start, CanStart);
		}

		private void Start(object obj)
		{
			_navigation.NavigateTo<LanguageViewModel>();
		}

		private bool CanStart(object obj)
		{
			return true;

		}
	}
}
