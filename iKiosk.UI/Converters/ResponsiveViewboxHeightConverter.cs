using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace iKiosk.UI.Converters
{
	public class ResponsiveViewboxHeightConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double screenHeight = SystemParameters.PrimaryScreenHeight;

			// Set height to 75 only for 800x600 resolution
			if (Math.Abs(screenHeight - 600) < 1)
				return 75.0;

			// For all other resolutions → auto height
			return double.NaN; // Equivalent to "Auto" in XAML
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> Binding.DoNothing;
	}
}
