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
	public class ResponsiveBottomMarginConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double screenHeight = SystemParameters.PrimaryScreenHeight;

			// Default margin (works for 1080p+)
			double bottomMargin = 15;

			// If the resolution height ≤ 600, reduce bottom margin
			if (screenHeight <= 600)
			{
				bottomMargin = 2;
			}

			// Return margin in "Left,Top,Right,Bottom" format
			return new Thickness(0, 0, 0, bottomMargin);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> Binding.DoNothing;
	}
}
