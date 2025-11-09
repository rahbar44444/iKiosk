using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace iKiosk.Framework.Wpf
{
	public class Command : ICommand
	{
		private readonly Action<object> _executeAction;

		private readonly Func<object, bool> _canExecuteAction;

		public Command(Action<object> executeAction, Func<object, bool> canExecuteAction)
		{
			//_executeAction = (o) => { try { executeAction(o); } catch (Exception ex) { Dispatcher.CurrentDispatcher.Invoke(() => HandleException(ex)); } };
			_executeAction = (o) => executeAction(o);
			_canExecuteAction = canExecuteAction;
		}
		protected virtual void HandleException(Exception ex)
		{
			//MessageBox
			MessageBox.Show($"{ex.Message}\n\n{ex.StackTrace}\n\n{ex.InnerException?.ToString()}");

		}
		public void Execute(object parameter) => _executeAction(parameter);
		public bool CanExecute(object parameter) => _canExecuteAction?.Invoke(parameter) ?? true;

		public event EventHandler CanExecuteChanged;

		public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);


	}
	public class Command<T> : ICommand
	{
		private readonly Action<T> _execute = null;
		private readonly Func<T, bool> _canExecute = null;

		public Command(Action<T> execute, Func<T, bool> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute ?? (_ => true);
		}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public bool CanExecute(object parameter) => _canExecute((T)parameter);

		public void Execute(object parameter) => _execute((T)parameter);
	}
}
