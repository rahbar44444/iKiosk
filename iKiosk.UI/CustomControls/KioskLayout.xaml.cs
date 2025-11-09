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

namespace iKiosk.UI.CustomControls
{
	public partial class KioskLayout : UserControl
	{
		public KioskLayout()
		{
			InitializeComponent();
		}

		//public static readonly DependencyProperty HeaderTextProperty =
		//	DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(KioskLayout), new PropertyMetadata(string.Empty));

		//public string HeaderText
		//{
		//	get => (string)GetValue(HeaderTextProperty);
		//	set => SetValue(HeaderTextProperty, value);
		//}

		//public static readonly DependencyProperty HeaderImageSourceProperty =
		//	DependencyProperty.Register(nameof(HeaderImageSource), typeof(ImageSource), typeof(KioskLayout), new PropertyMetadata(null));

		//public ImageSource HeaderImageSource
		//{
		//	get => (ImageSource)GetValue(HeaderImageSourceProperty);
		//	set => SetValue(HeaderImageSourceProperty, value);
		//}

		//public static readonly DependencyProperty IsHeaderVisibleProperty =
		//	DependencyProperty.Register(nameof(IsHeaderVisible), typeof(bool), typeof(KioskLayout), new PropertyMetadata(true));

		//public bool IsHeaderVisible
		//{
		//	get => (bool)GetValue(IsHeaderVisibleProperty);
		//	set => SetValue(IsHeaderVisibleProperty, value);
		//}
		public object HeaderContent
		{
			get => GetValue(HeaderContentProperty);
			set => SetValue(HeaderContentProperty, value);
		}

		public static readonly DependencyProperty HeaderContentProperty =
			DependencyProperty.Register(nameof(HeaderContent), typeof(object), typeof(KioskLayout), new PropertyMetadata(null));

		public object MainContent
		{
			get => GetValue(MainContentProperty);
			set => SetValue(MainContentProperty, value);
		}

		public static readonly DependencyProperty MainContentProperty =
			DependencyProperty.Register(nameof(MainContent), typeof(object), typeof(KioskLayout), new PropertyMetadata(null));
	}
}
