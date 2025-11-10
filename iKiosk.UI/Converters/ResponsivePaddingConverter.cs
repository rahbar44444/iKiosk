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
	public class ResponsivePaddingConverter : IValueConverter
	{
		public double BasePadding { get; set; } = 20.0;
		public double ReferenceWidth { get; set; } = 1920.0;
		public double MinPadding { get; set; } = 5.0;   // 👈 Reduced for 800×600

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double screenWidth = SystemParameters.PrimaryScreenWidth;
			double scale = screenWidth / ReferenceWidth;

			// Apply a *stronger* scaling curve for small resolutions
			// (Instead of linear scaling, use power function for smoother transitions)
			double adjustedScale = Math.Pow(scale, 1.25);

			double scaledPadding = BasePadding * adjustedScale;

			// Clamp minimum
			if (scaledPadding < MinPadding)
				scaledPadding = MinPadding;

			return new Thickness(scaledPadding);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> Binding.DoNothing;
	}
}
