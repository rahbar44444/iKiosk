using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace iKiosk.UI.Helper
{
	public static class ButtonHelper
	{
		public static readonly DependencyProperty CornerRadiusProperty =
			DependencyProperty.RegisterAttached(
				"CornerRadius",
				typeof(CornerRadius),
				typeof(ButtonHelper),
				new FrameworkPropertyMetadata(new CornerRadius(0), FrameworkPropertyMetadataOptions.AffectsRender));

		public static void SetCornerRadius(UIElement element, CornerRadius value)
		{
			element.SetValue(CornerRadiusProperty, value);
		}

		public static CornerRadius GetCornerRadius(UIElement element)
		{
			return (CornerRadius)element.GetValue(CornerRadiusProperty);
		}

		public static readonly DependencyProperty CustomBorderThicknessProperty =
			DependencyProperty.RegisterAttached(
				"CustomBorderThickness",
				typeof(Thickness),
				typeof(ButtonHelper),
				new FrameworkPropertyMetadata(new Thickness(1), FrameworkPropertyMetadataOptions.AffectsRender));

		public static void SetCustomBorderThickness(UIElement element, Thickness value)
		{
			element.SetValue(CustomBorderThicknessProperty, value);
		}

		public static Thickness GetCustomBorderThickness(UIElement element)
		{
			return (Thickness)element.GetValue(CustomBorderThicknessProperty);
		}
	}
}
