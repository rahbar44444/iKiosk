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
	public class ExpiredViewModel : ViewModelControlBase
	{
		private readonly IViewNavigation _navigation;
		public ICommand StartCommand { get; set; }
		public ICommand NavigateMainMenuCommand { get; }
		public ICommand UpdateCommand { get; }
		public ExpiredViewModel(IViewNavigation navigation)
		{
			_navigation = navigation;
			StartCommand = new Command(Start, CanStart);
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);
			UpdateCommand = new Command(Update, CanUpdate);
		}

		private void Start(object obj)
		{
			_navigation.NavigateTo<LanguageViewModel>();
		}

		private bool CanStart(object obj)
		{
			return true;

		}

		private void NavigateMainMenu(object obj)
		{
			_navigation.NavigateTo<HomeViewModel>();
		}

		private bool CanNavigateMainMenu(object obj)
		{
			return true;

		}

		private void Update(object obj)
		{
			_navigation.NavigateTo<ServiceViewModel>();
		}

		private bool CanUpdate(object obj)
		{
			return true;

		}

		
	}
}
