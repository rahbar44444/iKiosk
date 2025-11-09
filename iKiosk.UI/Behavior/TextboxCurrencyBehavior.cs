using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace iKiosk.UI.Behavior
{
	public static class TextBoxCurrencyBehavior
	{
		public static readonly DependencyProperty CurrencySuffixProperty =
			DependencyProperty.RegisterAttached(
				"CurrencySuffix",
				typeof(string),
				typeof(TextBoxCurrencyBehavior),
				new PropertyMetadata(string.Empty, OnCurrencySuffixChanged));

		public static void SetCurrencySuffix(DependencyObject element, string value)
			=> element.SetValue(CurrencySuffixProperty, value);

		public static string GetCurrencySuffix(DependencyObject element)
			=> (string)element.GetValue(CurrencySuffixProperty);

		private static void OnCurrencySuffixChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is not TextBox textBox) return;

			textBox.TextChanged -= TextBox_TextChanged;
			textBox.GotFocus -= TextBox_GotFocus;
			textBox.LostFocus -= TextBox_LostFocus;

			textBox.TextChanged += TextBox_TextChanged;
			textBox.GotFocus += TextBox_GotFocus;
			textBox.LostFocus += TextBox_LostFocus;
		}

		private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (sender is not TextBox textBox) return;
			var suffix = GetCurrencySuffix(textBox);
			if (string.IsNullOrEmpty(suffix)) return;

			if (textBox.IsFocused) return; // Don't modify during typing

			if (!textBox.Text.EndsWith($" {suffix}"))
			{
				textBox.Text = $"{textBox.Text.Trim()} {suffix}";
				textBox.CaretIndex = textBox.Text.Length - suffix.Length - 1;
			}
		}

		private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			if (sender is not TextBox textBox) return;
			var suffix = GetCurrencySuffix(textBox);
			if (textBox.Text.EndsWith($" {suffix}"))
				textBox.Text = textBox.Text.Replace($" {suffix}", "");
		}

		private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (sender is not TextBox textBox) return;
			var suffix = GetCurrencySuffix(textBox);
			if (!string.IsNullOrEmpty(textBox.Text) && !textBox.Text.EndsWith($" {suffix}"))
				textBox.Text = $"{textBox.Text.Trim()} {suffix}";
		}
	}
}
