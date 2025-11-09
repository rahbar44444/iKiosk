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
		#region Private Fields

		private readonly IViewNavigation _navigation;

		#endregion Private Fields

		#region Commands

		public ICommand StartCommand { get; set; }

		#endregion Commands

		#region Constructor

		public HomeViewModel(IViewNavigation navigation)
		{
			_navigation = navigation;
			StartCommand = new Command(Start, CanStart);
		}

		#endregion Constructor

		#region Private Methods

		private void Start(object obj)
		{
			_navigation.NavigateTo<LanguageViewModel>();
		}

		private bool CanStart(object obj)
		{
			return true;

		}

		#endregion Private Methods
	}
}
