using iKiosk.Framework.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iKiosk.UI.Views
{
	/// <summary>
	/// Interaction logic for PersonalDetailsView.xaml
	/// </summary>
	public partial class PersonalDetailsView : ViewControl
	{
		public PersonalDetailsView()
		{
			InitializeComponent();
		}

		private void TextBox_Loaded(object sender, RoutedEventArgs e)
		{
			if (sender is TextBox textBox)
			{
				string tagValue = textBox.Tag?.ToString();
				var txtBoxSaudiIqamaId = FindVisualChildren<TextBox>(this)
								.FirstOrDefault(t => (string)t.Tag == tagValue);
				txtBoxSaudiIqamaId.Focus();
			}
		}


		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj == null) yield break;
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
			{
				var child = VisualTreeHelper.GetChild(depObj, i);
				if (child is T t)
					yield return t;

				foreach (var childOfChild in FindVisualChildren<T>(child))
					yield return childOfChild;
			}
		}
	}
}
