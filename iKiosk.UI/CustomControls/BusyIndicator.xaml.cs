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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iKiosk.UI.CustomControls
{
	/// <summary>
	/// Interaction logic for BusyIndicator.xaml
	/// </summary>
	public partial class BusyIndicator : UserControl
	{
		public BusyIndicator()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty IsBusyProperty =
			DependencyProperty.Register(
				nameof(IsBusy),
				typeof(bool),
				typeof(BusyIndicator),
				new PropertyMetadata(false, OnIsBusyChanged));

		public bool IsBusy
		{
			get => (bool)GetValue(IsBusyProperty);
			set => SetValue(IsBusyProperty, value);
		}

		private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is BusyIndicator indicator)
			{
				bool isBusy = (bool)e.NewValue;
				indicator.Visibility = isBusy ? Visibility.Visible : Visibility.Collapsed;
			}
		}
	}
}

