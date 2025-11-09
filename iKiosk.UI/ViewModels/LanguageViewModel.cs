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
		//public string HeaderText { get; } 

		//public ImageSource HeaderImageSource { get; } = ImageSourceConstants.LanguageIconPath;

		private readonly IViewNavigation _navigation;
		private readonly IApiClient _apiClient;

		public ICommand NavigateMainMenuCommand { get; set; }


		private ObservableCollection<LanguageOption> _Languages;

		public ObservableCollection<LanguageOption> Languages
		{
			get { return _Languages; }
			set 
			{
				_Languages = value; 
				OnPropertyChanged("Languages");
			}
		}


		public ICommand SelectLanguageCommand { get; }

		public ICommand MainMenuCommand { get; }
		public ICommand BackCommand { get; }
		public ICommand NextCommand { get; }

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

		private bool _isBackVisible = false;
		public bool IsBackVisible
		{
			get => _isBackVisible;
			set
			{
				_isBackVisible = value;
				this.OnPropertyChanged("IsBackVisible");
			}
		}

		private bool _isNextVisible = false;
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



		public LanguageViewModel(IViewNavigation navigation, IApiClient apiClient)
		{
			//HeaderImageSource = new BitmapImage(
			//	new Uri(ImageSourceConstants.LanguageIconPath));
			_navigation = navigation;
			_apiClient = apiClient;
			NavigateMainMenuCommand = new Command(NavigateMainMenu, CanNavigateMainMenu);

			LoadLanguages();


			SelectLanguageCommand = new Command<LanguageOption>(OnLanguageSelected);
		}

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
	}
	
}
