using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using iKiosk.UI.Services;
using iKiosk.UI.Services.Api;
using iKiosk.UI.ViewModels;
using iKiosk.UI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace iKiosk.UI
{
	public class MainViewModel : ViewModelBase<MainWindow>, INotifyPropertyChanged
	{
		private object activeViewBase;
		private bool _progressVisibility = false;
		public event PropertyChangedEventHandler PropertyChanged;
		private readonly IMessageService _messageService;
		private readonly IApiClient _apiClient;
		public IViewNavigation _navigationService;

		public string WelcomeMessage { get; }

		public bool ProgressVisibility
		{
			get { return _progressVisibility; }
			set
			{
				_progressVisibility = value;
				this.NotifyPropertyChanged(nameof(ProgressVisibility));
			}
		}

		public object ActiveViewBase
		{
			get
			{
				return this.activeViewBase;
			}
			set
			{
				this.activeViewBase = value;
				this.NotifyPropertyChanged(nameof(ActiveViewBase));
			}
		}

		public Visibility IsGoBackEnabled
		{
			get
			{
				if (_navigationService.History.Count > 0)
					return Visibility.Visible;
				else
					return Visibility.Collapsed;
			}
		}

		public ICommand ShowMessageCommand { get; }

		public MainViewModel(IMessageService messageService, IViewNavigation navigationService, IApiClient apiClient)
		{
			_messageService = messageService;
			_apiClient = apiClient;
			_navigationService = navigationService;
			navigationService.PropertyChanged += Navigation_PropertyChanged;
			_navigationService.Register<HomeViewModel, HomeView>(_navigationService);
			_navigationService.Register<LanguageViewModel, LanguageView>(_navigationService, _apiClient);
			_navigationService.Register<PersonalDetailsViewModel, PersonalDetailsView>(_navigationService, _apiClient);
			_navigationService.Register<ExpiredViewModel, ExpiredView>(_navigationService, _apiClient);
			_navigationService.Register<ServiceViewModel, ServiceView>(_navigationService, _apiClient);
			_navigationService.Register<AmountCalculationViewModel, AmountCalculationView>(_navigationService, _apiClient);
			_navigationService.Register<PaymentMethodViewModel, PaymentMethodView>(_navigationService, _apiClient);
			_navigationService.Register<InsertCashViewModel, InsertCashView>(_navigationService, _apiClient);
		}

		private void NotifyPropertyChanged(string propertyName = "")
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void Navigation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(_navigationService.CurrentContent))
			{
				this.ActiveViewBase = _navigationService.CurrentContent.View.DataContext;

				// Showing different view on secondary display with respect to content shown on primary display
				//if (this.ActiveViewBase is LoginViewModel | this.ActiveViewBase is StartViewModel)
				//{
				//    this.mIEventAggregator?.PublishEvent<object>(new IntroductionViewModel(this.navigationService, this._apiOptions, currentSession));
				//}
				//	if (this.ActiveViewBase is StartViewModel)
				//	{
				//		this.mIEventAggregator?.PublishEvent<object>(new IntroductionViewModel(this.navigationService, this._apiOptions, currentSession));
				//	}
				//	else if (this.ActiveViewBase is LoginViewModel)
				//	{
				//		this.mIEventAggregator?.PublishEvent<object>(new HomeViewModel(this.navigationService, currentSession, mIEventAggregator, config));
				//	}
				//	else if (this.ActiveViewBase is QuotaViewModel | this.ActiveViewBase is Dialogs.OutcomeDialogViewModel |
				//		this.ActiveViewBase is CompletedPurchaseViewModel | this.ActiveViewBase is RestrictedQuotaViewModel |
				//		this.ActiveViewBase is QuotaExceedViewModel)
				//	{
				//		this.mIEventAggregator?.PublishEvent<object>(new ReminderViewModel(this.config));
				//	}
				//	else
				//	{
				//		this.mIEventAggregator?.PublishEvent<object>(this.ActiveViewBase);
				//	}

				//	var home = this.ActiveViewBase is HomeViewModel ? this.ActiveViewBase as HomeViewModel : null;
				//	if (home != null)
				//	{
				//		if (currentSession != null && currentSession.RefreshConfiguration)
				//		{
				//			RefreshConfiguration();
				//		}
				//	}
				//}

				this.OnPropertyChanged(() => this.IsGoBackEnabled);
			}
		}
	}
}
