using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace iKiosk.UI.CustomControls
{
	public enum IconPlacement
	{
		Left,
		Right,
		Top,
		Bottom
	}

	public class IconButton : Button
	{
		static IconButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton),
				new FrameworkPropertyMetadata(typeof(IconButton)));
		}

		#region Dependency Properties

		public static readonly DependencyProperty IconPathProperty =
			DependencyProperty.Register(nameof(IconPath), typeof(string), typeof(IconButton),
				new PropertyMetadata(null, OnIconPathChanged));

		public string IconPath
		{
			get => (string)GetValue(IconPathProperty);
			set => SetValue(IconPathProperty, value);
		}

		public static readonly DependencyProperty IconSourceProperty =
			DependencyProperty.Register(nameof(IconSource), typeof(ImageSource), typeof(IconButton));

		public ImageSource IconSource
		{
			get => (ImageSource)GetValue(IconSourceProperty);
			set => SetValue(IconSourceProperty, value);
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register(nameof(Text), typeof(string), typeof(IconButton),
				new FrameworkPropertyMetadata(string.Empty));

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public static readonly DependencyProperty IconWidthProperty =
			DependencyProperty.Register(nameof(IconWidth), typeof(double), typeof(IconButton),
				new FrameworkPropertyMetadata(50.0, OnIconSizeChanged));

		public double IconWidth
		{
			get => (double)GetValue(IconWidthProperty);
			set => SetValue(IconWidthProperty, value);
		}

		public static readonly DependencyProperty IconHeightProperty =
			DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(IconButton),
				new FrameworkPropertyMetadata(50.0, OnIconSizeChanged));

		public double IconHeight
		{
			get => (double)GetValue(IconHeightProperty);
			set => SetValue(IconHeightProperty, value);
		}

		public static readonly DependencyProperty IconPlacementProperty =
			DependencyProperty.Register(nameof(IconPlacement), typeof(IconPlacement), typeof(IconButton),
				new FrameworkPropertyMetadata(IconPlacement.Left));

		public IconPlacement IconPlacement
		{
			get => (IconPlacement)GetValue(IconPlacementProperty);
			set => SetValue(IconPlacementProperty, value);
		}

		public static readonly DependencyProperty IconSpacingProperty =
			DependencyProperty.Register(nameof(IconSpacing), typeof(double), typeof(IconButton),
				new FrameworkPropertyMetadata(10.0));

		public double IconSpacing
		{
			get => (double)GetValue(IconSpacingProperty);
			set => SetValue(IconSpacingProperty, value);
		}

		#endregion

		#region Helpers

		private static void OnIconPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is IconButton button &&
				e.NewValue is string path &&
				!string.IsNullOrWhiteSpace(path))
			{
				button.LoadImageFromPath(path);
			}
		}

		private static void OnIconSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is IconButton button && !string.IsNullOrWhiteSpace(button.IconPath))
				button.LoadImageFromPath(button.IconPath);
		}

		private void LoadImageFromPath(string path)
		{
			try
			{
				var bmp = new BitmapImage();
				bmp.BeginInit();
				bmp.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
				bmp.DecodePixelWidth = (int)Math.Max(1, IconWidth);
				bmp.DecodePixelHeight = (int)Math.Max(1, IconHeight);
				bmp.CacheOption = BitmapCacheOption.OnLoad;
				bmp.EndInit();
				bmp.Freeze();

				IconSource = bmp;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"[IconButton] Failed to load image: {ex.Message}");
			}
		}

		#endregion
	}
}
