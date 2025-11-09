using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using iKiosk.UI.Services.Api;
using System.Windows.Input;

namespace iKiosk.UI.ViewModels
{
	public class InsertCashViewModel : ViewModelControlBase
	{

		#region Private Fields

		private readonly IApiClient _apiClient;
		private readonly IViewNavigation _navigation;

		private string _NextButtonText = "Next";

		private decimal _RemainingAmount;
		private decimal _InsertedAmount;
		private decimal _TotalAmount;

		private bool _IsMainMenuVisible = true;
		private bool _IsBackVisible = true;
		private bool _IsNextVisible = true;

		#endregion Private Fields		

		#region Public Properties

		public bool IsMainMenuVisible
		{
			get => _IsMainMenuVisible;
			set
			{
				_IsMainMenuVisible = value;
				this.OnPropertyChanged("IsMainMenuVisible");
			}
		}

		public bool IsBackVisible
		{
			get => _IsBackVisible;
			set
			{
				_IsBackVisible = value;
				this.OnPropertyChanged("IsBackVisible");
			}
		}

		public bool IsNextVisible
		{
			get => _IsNextVisible;
			set
			{
				_IsNextVisible = value;
				this.OnPropertyChanged("IsNextVisible");
			}
		}

		public string NextButtonText
		{
			get => _NextButtonText;
			set
			{
				_NextButtonText = value;
				this.OnPropertyChanged("NextButtonText");
			}
		}

		public decimal TotalAmount
		{
			get => _TotalAmount;
			set { _TotalAmount = value; OnPropertyChanged(); CalculateRemaining(); }
		}

		public decimal InsertedAmount
		{
			get => _InsertedAmount;
			set { _InsertedAmount = value; OnPropertyChanged(); CalculateRemaining(); }
		}

		public decimal RemainingAmount
		{
			get => _RemainingAmount;
			private set { _RemainingAmount = value; OnPropertyChanged(); }
		}

		#endregion Public Properties

		#region Commands

		public ICommand NavigateMainMenuCommand { get; }
		public ICommand NavigateNextCommand { get; }
		public ICommand NavigateBackCommand { get; }

		#endregion Commands

		#region Constructor

		public InsertCashViewModel(IViewNavigation navigation, ApiClient apiClient)
		{
			_apiClient = apiClient;
			_navigation = navigation;
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);
			NavigateNextCommand = new Command(NavigateNext, CanNavigateNext);
			NavigateBackCommand = new Command(NavigateBack, CanNavigateBack);
		}

		#endregion Constructor

		#region Private Methods

		/// <summary>
		/// Calculate Remaining
		/// </summary>
		private void CalculateRemaining()
		{
			RemainingAmount = Math.Max(0, TotalAmount - InsertedAmount);
		}

		/// <summary>
		/// Navigate to Main Menu
		/// </summary>
		/// <param name="obj"></param>
		private void NavigateMainMenu(object obj)
		{
			_navigation.NavigateTo<HomeViewModel>();
		}		

		/// <summary>
		/// Navigate to Result Screen
		/// </summary>
		/// <param name="obj"></param>
		private void NavigateNext(object obj)
		{
			_navigation.NavigateTo<AmountCalculationViewModel>();
		}

		/// <summary>
		/// Navigate back to Payment Method screen
		/// </summary>
		/// <param name="obj"></param>
		private void NavigateBack(object obj)
		{
			_navigation.NavigateBack();
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
