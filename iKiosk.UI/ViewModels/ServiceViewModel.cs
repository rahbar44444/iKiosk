using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using iKiosk.UI.Extensions;
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
	public class ServiceViewModel : ViewModelControlBase
	{
		public readonly IApiClient _apiClient;
		private readonly IViewNavigation _navigation;

		private ObservableCollection<ServiceOption> _Services;

		public ObservableCollection<ServiceOption> Services
		{
			get { return _Services; }
			set 
			{ 
				_Services = value;
				OnPropertyChanged("Services");
			}
		}

		private ServiceOption _SelectedService;

		public ServiceOption SelectedService
		{
			get => _SelectedService;
			set
			{

					_SelectedService = value;

					OnPropertyChanged(nameof(SelectedService));
			}
		}


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
				

		private bool _IsNextEnabled = false;
		public bool IsNextEnabled
		{
			get => _IsNextEnabled;
			set
			{
				_IsNextEnabled = value;
				this.OnPropertyChanged("IsNextEnabled");
			}
		}



		public ServiceViewModel(IViewNavigation navigation, IApiClient apiClient)
		{
			_apiClient = apiClient;
			_navigation = navigation;
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);
			NavigateNextCommand = new Command(NavigateNext, CanNavigateNext);
			NavigateBackCommand = new Command(NavigateBack, CanNavigateBack);

			LoadServices();


			SelectedServiceCommand = new Command<ServiceOption>(OnServiceSelected);
		}

		private async Task LoadServices()
		{
			var result = await _apiClient.GetServicesAsync();

			if (!result.HasError && result.Data != null)
			{
				await Application.Current.Dispatcher.InvokeAsync(() =>
				{
					Services = result.Data.ToObservableCollection();
				});
			}

		}

		private void OnServiceSelected(ServiceOption selectedService)
		{
			if (selectedService == null)
				return;

			foreach (var service in Services)
				service.IsSelected = false;

			// Select the clicked one
			selectedService.IsSelected = true;

			IsNextEnabled = true;
		}

		private async void NavigateMainMenu(object obj)
		{
			await RunCommand(() => ProgressVisibility, async () =>
			{
				await Task.Delay(300);
				_navigation.NavigateTo<HomeViewModel>();
			});
		}

		private bool CanNavigateMainMenu(object obj)
		{
			return true;

		}

		private async void NavigateNext(object obj)
		{
			await RunCommand(() => ProgressVisibility, async () =>
			{
				await Task.Delay(200);
				SelectedService = Services.FirstOrDefault(s => s.IsSelected);
				_navigation.NavigateTo<AmountCalculationViewModel>(SelectedService);
			});
		}

		private bool CanNavigateNext(object obj)
		{
			return true;

		}

		private async void NavigateBack(object obj)
		{
			await RunCommand(() => ProgressVisibility, async () =>
			{
				await Task.Delay(300);
				_navigation.NavigateBack();
			});
		}

		private bool CanNavigateBack(object obj)
		{
			return true;

		}
	}
}
