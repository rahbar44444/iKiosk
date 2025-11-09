using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace iKiosk.UI.CustomControls
{
	public partial class NumberKeypadControl : UserControl
	{
		private static TextBox _activeTextBox;

		public NumberKeypadControl()
		{
			InitializeComponent();

			// Global keyboard focus tracker
			EventManager.RegisterClassHandler(typeof(TextBox),
				UIElement.GotFocusEvent,
				new RoutedEventHandler(OnAnyTextBoxGotFocus));
		}

		private static void OnAnyTextBoxGotFocus(object sender, RoutedEventArgs e)
		{
			_activeTextBox = sender as TextBox;
		}

		private void OnKeyClicked(object sender, RoutedEventArgs e)
		{
			if (sender is Button btn && btn.Content is string key)
			{
				HandleKeyInput(key);
			}
		}

		private void HandleKeyInput(string key)
		{
			if (_activeTextBox == null) return;

			if (key == "Del")
			{
				if (_activeTextBox.SelectionStart > 0)
				{
					int index = _activeTextBox.SelectionStart;
					_activeTextBox.Text = _activeTextBox.Text.Remove(index - 1, 1);
					_activeTextBox.SelectionStart = index - 1;
				}
			}
			else
			{
				int index = _activeTextBox.SelectionStart;
				_activeTextBox.Text = _activeTextBox.Text.Insert(index, key);
				_activeTextBox.SelectionStart = index + 1;
			}

			if (_activeTextBox.Tag?.ToString() == "txtDateOfBirth")
			{
				_activeTextBox.Text = FormatDateInput(_activeTextBox.Text);
				_activeTextBox.SelectionStart = _activeTextBox.Text.Length;
			}
		}

		private string FormatDateInput(string input)
		{
			input = new string(input.Where(char.IsDigit).ToArray());

			if (input.Length > 8)
				input = input.Substring(0, 8);

			if (input.Length > 4)
				return $"{input.Substring(0, 2)}/{input.Substring(2, 2)}/{input.Substring(4)}";
			if (input.Length > 2)
				return $"{input.Substring(0, 2)}/{input.Substring(2)}";

			return input;
		}
	}
}
