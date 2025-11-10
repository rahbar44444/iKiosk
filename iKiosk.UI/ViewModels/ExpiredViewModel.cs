using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using iKiosk.UI.Services.Api;
using iKiosk.UI.Services.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace iKiosk.UI.ViewModels
{
	public class ExpiredViewModel : ViewModelControlBase
	{
		#region Private Fields

		private readonly IApiClient _apiClient;
		private readonly IViewNavigation _navigation;

		PersonalDetailResponse _personalDetails;

		private string _PersonName;

		#endregion Private Fields

		#region Public Properties

		public string PersonName
		{
			get { return _PersonName; }
			set 
			{ 
				_PersonName = value; 
				OnPropertyChanged(nameof(PersonName));
			}
		}
		public string FormattedExpiredMessage
		{
			get
			{
				string template = (string)Application.Current.Resources["MainContent_ExpiredMessage"];
				return string.Format(template, PersonName);
			}
		}


		#endregion Public Properties

		#region Commands

		public ICommand NavigateMainMenuCommand { get; }
		public ICommand UpdateCommand { get; }

		#endregion Commands

		#region Constructor

		public ExpiredViewModel(IViewNavigation navigation, IApiClient apiClient)
		{
			_apiClient = apiClient;
			_navigation = navigation;
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);
			UpdateCommand = new Command(Update, CanUpdate);
		}

		#endregion Constructor

		#region Public Methods

		public override void ViewModelLoaded(object message)
		{
			_personalDetails = (PersonalDetailResponse)message;
			if (_personalDetails != null)
			{
				PersonName = _personalDetails.FullName;
			}
		}

		#endregion Public Methods

		#region Private Methods

		private async void NavigateMainMenu(object obj)
		{
			await RunCommand(() => ProgressVisibility, async () =>
			{
				await Task.Delay(300);
				_navigation.NavigateTo<HomeViewModel>();
			});
		}


		private async void Update(object obj)
		{
			await RunCommand(() => ProgressVisibility, async () =>
			{
				await Task.Delay(300);
				_navigation.NavigateTo<PersonalDetailsViewModel>(_personalDetails);
			});
		}

		private bool CanNavigateMainMenu(object obj)
		{
			return true;

		}
		private bool CanUpdate(object obj)
		{
			return true;

		}

		#endregion Private Methods

	}
}
