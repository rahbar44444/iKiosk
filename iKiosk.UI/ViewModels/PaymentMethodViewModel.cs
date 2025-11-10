using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using iKiosk.UI.Helper;
using iKiosk.UI.Services.Api;
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
		#region Private Fields

		private readonly IApiClient _apiClient;
		private readonly IViewNavigation _navigation;

		private string _SelectedPaymentMethod;
		private string _NextButtonText = "Next";

		private bool _IsMainMenuVisible = true;
		private bool _IsNextVisible = true;
		private bool _IsBackVisible = true;

		#endregion Private Fields

		#region Public Properties

		public bool IsMainMenuVisible
		{
			get { return _IsMainMenuVisible; }
			set
			{
				_IsMainMenuVisible = value;
				this.OnPropertyChanged("IsMainMenuVisible");
			}
		}

		public bool IsBackVisible
		{
			get { return _IsBackVisible; }
			set
			{
				_IsBackVisible = value;
				this.OnPropertyChanged("IsBackVisible");
			}
		}

		public bool IsNextVisible
		{
			get { return _IsNextVisible; }
			set
			{
				_IsNextVisible = value;
				this.OnPropertyChanged("IsNextVisible");
			}
		}

		public string NextButtonText
		{
			get { return _NextButtonText; }
			set
			{
				_NextButtonText = value;
				this.OnPropertyChanged("NextButtonText");
			}
		}

		public string SelectedPaymentMethod
		{
			get { return _SelectedPaymentMethod; }
			set
			{
				if (_SelectedPaymentMethod != value)
				{
					_SelectedPaymentMethod = value;
					OnPropertyChanged(nameof(SelectedPaymentMethod));
				}
			}
		}

		#endregion Public Properties

		#region Commands

		public ICommand SelectedServiceCommand { get; }

		public ICommand NavigateMainMenuCommand { get; }
		public ICommand NavigateNextCommand { get; }
		public ICommand NavigateBackCommand { get; }
		public ICommand PayByCashCommand { get; set; }
		public ICommand PayByCardCommand { get; set; }

		#endregion Commands

		#region Constructor

		public PaymentMethodViewModel(IViewNavigation navigation, IApiClient apiClient)
		{
			_apiClient=apiClient;
			_navigation = navigation;
			PayByCashCommand = new Command(PayByCash, CanPayByCash);
			PayByCardCommand = new Command(PayByCard, CanPayByCard);
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);
			NavigateNextCommand = new Command(NavigateNext, CanNavigateNext);
			NavigateBackCommand = new Command(NavigateBack, CanNavigateBack);
		}

		#endregion Constructor

		#region Private Methods	

		private async void NavigateMainMenu(object obj)
		{
			await RunCommand(() => ProgressVisibility, async () =>
			{
				await Task.Delay(300);
				_navigation.NavigateTo<HomeViewModel>();
			});
		}
		private async void NavigateNext(object obj)
		{
			await RunCommand(() => ProgressVisibility, async () =>
			{
				await Task.Delay(300);
				_navigation.NavigateTo<InsertCashViewModel>();
			});
		}
		private async void NavigateBack(object obj)
		{
			await RunCommand(() => ProgressVisibility, async () =>
			{
				await Task.Delay(300);
				_navigation.NavigateBack();
			});
		}

		private void PayByCash(object obj)
		{
			SelectedPaymentMethod = "Cash";
		}

		private void PayByCard(object obj)
		{
			SelectedPaymentMethod = "Card";
		}

		private bool CanPayByCard(object obj)
		{
			return true;
		}
		private bool CanPayByCash(object obj)
		{
			return true;
		}

		private bool CanNavigateMainMenu(object obj)
		{
			return true;

		}

		private bool CanNavigateNext(object obj)
		{
			return true;

		}

		private bool CanNavigateBack(object obj)
		{
			return true;

		}

		#endregion Private Methods
	}

}
