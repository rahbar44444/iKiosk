using iKiosk.Framework.Wpf;
using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using iKiosk.UI.Constants;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace iKiosk.UI.ViewModels
{
	public class LanguageViewModel : ViewModelControlBase
	{
		#region Private Fields

		private readonly IViewNavigation _navigation;
		private readonly IApiClient _apiClient;

		private ObservableCollection<LanguageOption> _Languages;

		private string _NextButtonText = "Next";

		private bool _IsMainMenuVisible = true;
		private bool _IsBackVisible = false;
		private bool _IsNextVisible = false;

		#endregion Private Fields

		#region Public Properties

		public ObservableCollection<LanguageOption> Languages
		{
			get { return _Languages; }
			set 
			{
				_Languages = value; 
				OnPropertyChanged("Languages");
			}
		}

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

		#endregion Public Properties

		#region Commands

		public ICommand NavigateMainMenuCommand { get; set; }
		public ICommand SelectLanguageCommand { get; }

		#endregion Commands

		#region Constructor

		public LanguageViewModel(IViewNavigation navigation, IApiClient apiClient)
		{
			_navigation = navigation;
			_apiClient = apiClient;
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);
			LoadLanguages();
			SelectLanguageCommand = new Command<LanguageOption>(OnLanguageSelected);
		}

		#endregion Constructor

		#region Private Methods

		private async Task LoadLanguages()
		{
			var result = await _apiClient.GetLanguagesAsync();

			if (!result.HasError && result.Data != null)
			{
				await Application.Current.Dispatcher.InvokeAsync(() =>
				{
					Languages = result.Data.ToObservableCollection();
				});
			}

		}

		private void OnLanguageSelected(LanguageOption selectedLanguage)
		{
			if (selectedLanguage == null)
				return;

			try
			{
				var cultureCode = selectedLanguage.Name switch
				{
					"العربية" => "ar",
					"English" => "en",
					"اردو" => "ur",
					"हिंदी" => "hi",
					"മലയാളം" => "ml",
					"Filipino" => "fil",
					"French" => "fr",
					"Spanish" => "es",
					_ => "en"
				};

				// Change Language
				LocalizationManager.ChangeLanguage(cultureCode);

				// Navigation to Personal Details
				_navigation.NavigateTo<PersonalDetailsViewModel>();

			}
			catch (Exception ex)
			{
				
			}
		}

		private void NavigateMainMenu(object obj)
		{
			_navigation.NavigateBack();
		}

		private bool CanNavigateMainMenu(object obj)
		{
			return true;

		}

		#endregion Private Methods
	}

}
