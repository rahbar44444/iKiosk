using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace iKiosk.Framework.Wpf.Helpers
{
	public static class ResolutionHelper
	{
		// Base design resolution (match what you designed your kiosk for)
		public const double BaseWidth = 1920;
		public const double BaseHeight = 1080;

		private static double ScreenScaleX => SystemParameters.PrimaryScreenWidth / BaseWidth;
		private static double ScreenScaleY => SystemParameters.PrimaryScreenHeight / BaseHeight;

		// Use uniform scaling to keep aspect ratio consistent
		private static double ScaleFactor => Math.Min(ScreenScaleX, ScreenScaleY);

		public static void ApplyScale(FrameworkElement element)
		{
			if (element == null) return;
			element.LayoutTransform = new ScaleTransform(ScaleFactor, ScaleFactor);
		}

		// Attached property so you can enable scaling from XAML
		public static readonly DependencyProperty EnableAutoScaleProperty =
			DependencyProperty.RegisterAttached(
				"EnableAutoScale",
				typeof(bool),
				typeof(ResolutionHelper),
				new PropertyMetadata(false, OnEnableAutoScaleChanged));

		public static void SetEnableAutoScale(DependencyObject d, bool value)
			=> d.SetValue(EnableAutoScaleProperty, value);

		public static bool GetEnableAutoScale(DependencyObject d)
			=> (bool)d.GetValue(EnableAutoScaleProperty);

		private static void OnEnableAutoScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is not FrameworkElement fe || (bool)e.NewValue == false) return;

			void Reapply() => ApplyScale(fe);

			// Apply on load
			fe.Loaded += (s, _) => Reapply();

			// If this is a Window, reapply when resizing/moving/monitor changes
			if (fe is Window win)
			{
				win.SizeChanged += (s, _) => Reapply();
				win.LocationChanged += (s, _) => Reapply();
			}

			// When display settings change (resolution/DPI change)
			SystemEvents.DisplaySettingsChanged += (s, _) =>
			{
				Application.Current?.Dispatcher?.Invoke(() =>
				{
					if (Application.Current?.MainWindow != null)
						ApplyScale(Application.Current.MainWindow);
				});
			};
		}

		// Optional: helper if you want to scale font sizes in code
		public static double GetScaledFont(double baseSize) => baseSize * ScaleFactor;
	}
}
