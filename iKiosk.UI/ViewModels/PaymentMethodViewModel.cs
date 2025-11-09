using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using iKiosk.UI.Helper;
using iKiosk.UI.Services.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace iKiosk.UI.ViewModels
{
	public class PaymentMethodViewModel : ViewModelControlBase
	{
		private readonly IViewNavigation _navigation;

		public ObservableCollection<ServiceOption> Services { get; set; }

		public ICommand SelectedServiceCommand { get; }

		public ICommand NavigateMainMenuCommand { get; }
		public ICommand NavigateNextCommand { get; }
		public ICommand NavigateBackCommand { get; }

		private bool _isMainMenuVisible = true;
		public bool IsMainMenuVisible
		{
			get => _isMainMenuVisible;
			set
			{
				_isMainMenuVisible = value;
				this.OnPropertyChanged("IsMainMenuVisible");
			}
		}

		private bool _isBackVisible = true;
		public bool IsBackVisible
		{
			get => _isBackVisible;
			set
			{
				_isBackVisible = value;
				this.OnPropertyChanged("IsBackVisible");
			}
		}

		private bool _isNextVisible = true;
		public bool IsNextVisible
		{
			get => _isNextVisible;
			set
			{
				_isNextVisible = value;
				this.OnPropertyChanged("IsNextVisible");
			}
		}

		private string _nextButtonText = "Next";
		public string NextButtonText
		{
			get => _nextButtonText;
			set
			{
				_nextButtonText = value;
				this.OnPropertyChanged("NextButtonText");
			}
		}



		public PaymentMethodViewModel(IViewNavigation navigation)
		{
			_navigation = navigation;
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);
			NavigateNextCommand = new Command(NavigateNext, CanNavigateNext);
			NavigateBackCommand = new Command(NavigateBack, CanNavigateBack);

			Services = new ObservableCollection<ServiceOption>
		{
			new ServiceOption { Name = "Money Transfer" },
			new ServiceOption { Name = "Bill Payment" },
			new ServiceOption { Name = "Mobile Recharge" },
			new ServiceOption { Name = "Open Account" },
			new ServiceOption { Name = "Update ID" },
			new ServiceOption { Name = "Other Services" }
		};


			SelectedServiceCommand = new Command<ServiceOption>(OnServiceSelected);
		}
		private void OnServiceSelected(ServiceOption selectedService)
		{
			if (selectedService == null)
				return;

			
		}

		private void NavigateMainMenu(object obj)
		{
			_navigation.NavigateTo<HomeViewModel>();
		}

		private bool CanNavigateMainMenu(object obj)
		{
			return true;

		}

		private void NavigateNext(object obj)
		{
			_navigation.NavigateTo<InsertCashViewModel>();
		}

		private bool CanNavigateNext(object obj)
		{
			return true;

		}

		private void NavigateBack(object obj)
		{
			_navigation.NavigateBack();
		}

		private bool CanNavigateBack(object obj)
		{
			return true;

		}
	}

}
