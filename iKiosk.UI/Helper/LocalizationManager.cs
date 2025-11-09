using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace iKiosk.UI.Helper
{
	public static class LocalizationManager
	{
		private const string ResourcePrefix = "pack://application:,,,/iKiosk.UI;component/Resources/StringResources.";

		public static void ChangeLanguage(string cultureCode)
		{
			var appResources = Application.Current.Resources.MergedDictionaries;

			// Create new language dictionary
			var newLangDict = new ResourceDictionary
			{
				Source = new Uri($"{ResourcePrefix}{cultureCode}.xaml", UriKind.Absolute)
			};

			// Find old language dictionary (the one that starts with "StringResources.")
			var oldLangDict = appResources.FirstOrDefault(d =>
				d.Source != null && d.Source.OriginalString.Contains("StringResources."));

			// Replace or insert language dictionary
			if (oldLangDict != null)
			{
				int index = appResources.IndexOf(oldLangDict);
				appResources.Remove(oldLangDict);
				appResources.Insert(index, newLangDict);
			}
			else
			{
				appResources.Add(newLangDict);
			}
		}
	}
}
