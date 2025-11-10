using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace iKiosk.UI.Behavior
{
	public static class ZIndexBehavior
	{
		public static readonly DependencyProperty BringToFrontOnPressProperty =
			DependencyProperty.RegisterAttached(
				"BringToFrontOnPress",
				typeof(bool),
				typeof(ZIndexBehavior),
				new PropertyMetadata(false, OnBringToFrontOnPressChanged));

		public static bool GetBringToFrontOnPress(DependencyObject obj) =>
			(bool)obj.GetValue(BringToFrontOnPressProperty);

		public static void SetBringToFrontOnPress(DependencyObject obj, bool value) =>
			obj.SetValue(BringToFrontOnPressProperty, value);

		private static void OnBringToFrontOnPressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is UIElement element)
			{
				if ((bool)e.NewValue)
				{
					element.PreviewMouseDown += Element_PreviewMouseDown;
					element.PreviewMouseUp += Element_PreviewMouseUp;
					element.LostMouseCapture += Element_LostMouseCapture;
				}
				else
				{
					element.PreviewMouseDown -= Element_PreviewMouseDown;
					element.PreviewMouseUp -= Element_PreviewMouseUp;
					element.LostMouseCapture -= Element_LostMouseCapture;
				}
			}
		}

		private static void Element_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is UIElement element && VisualTreeHelper.GetParent(element) is Panel panel)
			{
				// reset all siblings
				foreach (UIElement child in panel.Children)
					Panel.SetZIndex(child, 0);

				// bring this one to front
				Panel.SetZIndex(element, 999);
			}
		}

		private static void Element_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (sender is UIElement element)
				Panel.SetZIndex(element, 0);
		}

		private static void Element_LostMouseCapture(object sender, MouseEventArgs e)
		{
			if (sender is UIElement element)
				Panel.SetZIndex(element, 0);
		}
	}
}
