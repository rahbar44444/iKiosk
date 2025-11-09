using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace iKiosk.UI.Converters
{
	public class DivideBy100Converter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> (double)value / 100.0;
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> (double)value * 100.0;
	}

}
