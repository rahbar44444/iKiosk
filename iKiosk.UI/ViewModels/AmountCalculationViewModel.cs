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
	public class AmountCalculationViewModel : ViewModelControlBase
	{
		#region Private Fields

		private readonly IApiClient _apiClient;
		private readonly IViewNavigation _navigation;

		private string _NextButtonText = "Next";

		private decimal _ExchangeRate = 100m; // 1 INR = 0.045 SAR
		private decimal _Fee = 25m;
		private decimal _ValueAddedTax;
		private decimal _AmountToPay;

		private bool _IsMainMenuVisible = true;
		private bool _IsBackVisible = true;
		private bool _IsNextVisible = true;

		#endregion Private Fields 		

		#region Public Properties

		public decimal ExchangeRate
		{
			get { return _ExchangeRate; }
			set
			{
				_ExchangeRate = value;
				OnPropertyChanged(nameof(ExchangeRate));
				CalculateTotals();
			}
		}

		public decimal Fee
		{
			get { return _Fee; }
			set
			{
				_Fee = value;
				OnPropertyChanged(nameof(Fee));
				CalculateTotals();
			}
		}

		public decimal ValueAddedTax
		{
			get { return _ValueAddedTax; }
		    set
			{
				_ValueAddedTax = value;
				OnPropertyChanged(nameof(ValueAddedTax));
				CalculateTotals();
			}
		}

		public decimal AmountToPay
		{
			get { return _AmountToPay; }
		    set
			{
				_AmountToPay = value;
				OnPropertyChanged(nameof(AmountToPay));
			}
		}

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

		#endregion Public Properties

		#region Commands

		public ICommand SelectedServiceCommand { get; }
		public ICommand NavigateMainMenuCommand { get; }
		public ICommand NavigateNextCommand { get; }
		public ICommand NavigateBackCommand { get; }

		#endregion Commands

		#region Constructor

		public AmountCalculationViewModel(IViewNavigation navigation, IApiClient apiClient)
		{
			_apiClient = apiClient;
			_navigation = navigation;
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);
			NavigateNextCommand = new Command(NavigateNext, CanNavigateNext);
			NavigateBackCommand = new Command(NavigateBack, CanNavigateBack);
			CalculateTotals();
		}

		#endregion Constructor

		#region Public Methods
		public override void ViewModelLoaded(object message)
		{
			
		}

		#endregion Public Methods

		#region Private Methods

		private async void CalculateTotals()
		{
			var response = await _apiClient.CalculateRemittanceAsync(new RemittanceCalculationRequest
			{
				ExchangeRate = ExchangeRate,
				Fee = Fee,
				VatRate = ValueAddedTax,
				AmountToSend = AmountToPay
			}); 
			ValueAddedTax = response.Data.ValueAddedTax;
			AmountToPay = response.Data.AmountToPay;
		}

		private void NavigateMainMenu(object obj)
		{
			_navigation.NavigateTo<HomeViewModel>();
		}

		private void NavigateNext(object obj)
		{
			_navigation.NavigateTo<PaymentMethodViewModel>();
		}

		private void NavigateBack(object obj)
		{
			_navigation.NavigateBack();
		}

		private bool CanNavigateNext(object obj)
		{
			return true;

		}
		private bool CanNavigateMainMenu(object obj)
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
