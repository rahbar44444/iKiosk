using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using iKiosk.UI.Services.Api;
using iKiosk.UI.Services.Models;
using System.Globalization;
using System.Windows.Input;

namespace iKiosk.UI.ViewModels
{
	public class PersonalDetailsViewModel : ViewModelControlBase
	{
		#region Private Fields

		private readonly IApiClient _apiClient;
		private readonly IViewNavigation _navigation;

		private ApiResult<PersonalDetailResponse> _response;

		private string _NextButtonText = "Next";

		private int? _SaudiIqamaId;

		private string _DateOfBirth;

		private bool _IsMainMenuVisible = true;
		private bool _IsBackVisible = true;
		private bool _IsNextVisible = true;
		private bool IsValidDOB = false;
		private bool IsSaudiIqamaValid = false;
		private bool _IsNextEnabled = false;

		#endregion Private Fields

		#region Public Properties

		public int? SaudiIqamaId
		{
			get { return _SaudiIqamaId; }
			set 
			{ 
				_SaudiIqamaId = value; 
				OnPropertyChanged("SaudiIqamaId");
				IsNextEnabled = _SaudiIqamaId.ToString().Length == 10;
			}
		}

		public string DateOfBirth
		{
			get { return _DateOfBirth; }
			set 
			{ 
				_DateOfBirth = value; 
				OnPropertyChanged("DateOfBirth");
				IsNextEnabled= DateTime.TryParseExact(
				DateOfBirth,
				"dd/MM/yyyy",
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out DateTime dateOfBirth);
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

		public bool IsNextEnabled
		{
			get { return _IsNextEnabled; }
			set 
			{ 
				_IsNextEnabled = value;
				OnPropertyChanged("IsNextEnabled");
			}
		}


		#endregion Public Properties

		#region Commands

		public ICommand NavigateMainMenuCommand { get; }
		public ICommand NavigateNextCommand { get; }
		public ICommand NavigateBackCommand { get; }
		public ICommand RemittanceCommand { get; set; }

		#endregion Commands		

		#region Constructor

		public PersonalDetailsViewModel(IViewNavigation navigation, IApiClient apiClient)
		{
			_navigation = navigation;
			_apiClient = apiClient;
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);
			NavigateNextCommand = new Command(NavigateNext, CanNavigateNext);
			NavigateBackCommand = new Command(NavigateBack, CanNavigateBack);
			RemittanceCommand = new Command(RemittanceCalculator, CanCalculateRemittance);
		}

		#endregion Constructor

		#region Private Methods

		private void NavigateMainMenu(object obj)
		{
			_navigation.NavigateTo<HomeViewModel>();
		}

		private async void NavigateNext(object obj)
		{
			await RunCommand(() => ProgressVisibility, async () =>
			{
				if (!DateTime.TryParseExact(
						DateOfBirth,
						"dd/MM/yyyy",
						CultureInfo.InvariantCulture,
						DateTimeStyles.None,
						out DateTime dateOfBirth))
				{
					return;
				}

				await Task.Delay(2000);

				var response = await _apiClient.VerifyPersonalDetailsAsync(
					new PersonalDetailRequest
					{
						SaudiId = SaudiIqamaId.GetValueOrDefault(),
						DateOfBirth = dateOfBirth
					});

				if (response?.Data is null)
					return;

				if (!response.Data.IsValid)
				{
					_navigation.NavigateTo<ExpiredViewModel>(response.Data);
				}
				else
				{
					_navigation.NavigateTo<ServiceViewModel>(response.Data);
				}
			});
		}

		private void NavigateBack(object obj)
		{
			_navigation.NavigateTo<HomeViewModel>();
		}
		private void RemittanceCalculator(object obj)
		{
			_navigation.NavigateTo<AmountCalculationViewModel>(_response?.Data);
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

		private bool CanCalculateRemittance(object obj)
		{
			return true;
		}

		#endregion Private Methods
	}

}
