using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using iKiosk.UI.Services.Api;
using iKiosk.UI.Services.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace iKiosk.UI.ViewModels
{
	public class PersonalDetailsViewModel : ViewModelControlBase
	{
		private readonly IApiClient _apiClient;
		private readonly IViewNavigation _navigation;

		private ApiResult<PersonalDetailResponse> _response;

		private int _SaudiIqamaId;

		public int SaudiIqamaId
		{
			get { return _SaudiIqamaId; }
			set 
			{ 
				_SaudiIqamaId = value; 
				OnPropertyChanged("SaudiIqamaId");
			}
		}

		private string _DateOfBirth;

		public string DateOfBirth
		{
			get { return _DateOfBirth; }
			set 
			{ 
				_DateOfBirth = value; 
				OnPropertyChanged("DateOfBirth");
			}
		}

		public ICommand NavigateMainMenuCommand { get; }
		public ICommand NavigateNextCommand { get; }
		public ICommand NavigateBackCommand { get; }
		public ICommand RemittanceCommand { get; set; }

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



		public PersonalDetailsViewModel(IViewNavigation navigation, IApiClient apiClient)
		{
			_navigation = navigation;
			_apiClient = apiClient;
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);
			NavigateNextCommand = new Command(NavigateNext, CanNavigateNext);
			NavigateBackCommand = new Command(NavigateBack, CanNavigateBack);
			RemittanceCommand = new Command(RemittanceCalculator, CanCalculateRemittance);
		}

		private void NavigateMainMenu(object obj)
		{
			_navigation.NavigateTo<HomeViewModel>();
		}

		private bool CanNavigateMainMenu(object obj)
		{
			return true;

		}

		private async void NavigateNext(object obj)
		{
			_response = await _apiClient.VerifyPersonalDetailsAsync(new PersonalDetailRequest { SaudiId= SaudiIqamaId, DateOfBirth= DateTime.Now });

			if (_response is null)
				return;

			if (!_response.Data.IsValid)
			{
				_navigation.NavigateTo<ExpiredViewModel>(_response.Data);
			}
			else
			{
				_navigation.NavigateTo<ServiceViewModel>(_response.Data);
			}
		}

		private bool CanNavigateNext(object obj)
		{
			return true;

		}

		private void NavigateBack(object obj)
		{
			_navigation.NavigateTo<HomeViewModel>();
		}

		private bool CanNavigateBack(object obj)
		{
			return true;

		}

		private void RemittanceCalculator(object obj)
		{
			_navigation.NavigateTo<AmountCalculationViewModel>(_response.Data);
		}

		private bool CanCalculateRemittance(object obj)
		{
			return true;
		}
	}
	
}
