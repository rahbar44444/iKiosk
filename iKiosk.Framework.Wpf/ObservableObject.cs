using iKiosk.Framework.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Framework.Wpf
{
	public class ObservableObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableObject()
		{
		}

		/// <summary>
		///     Updates the <paramref name="field" /> with <paramref name="newValue" /> if it is different,
		///     and raise <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="field"></param>
		/// <param name="newValue"></param>
		/// <param name="propertyName">Name of the property calling this method - <see cref="CallerMemberNameAttribute" /></param>
		/// <returns>
		///     True if <paramref name="field" /> is different and updated with <paramref name="newValue" />, 
		///     False if value is the same and nothing is changed or updated.
		/// </returns>
		protected bool SetPropertyField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
		{

			if (!EqualityComparer<T>.Default.Equals(field, newValue))
			{
				field = newValue;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
				return true;
			}
			return false;
		}

		/// <summary>
		///     Raises <see cref="INotifyPropertyChanged.PropertyChanged" /> event for the property
		///     specified in <paramref name="exp" />.
		/// </summary>
		/// <example>
		///     Caller has a property named "SomeProperty", in the setter, call the method to raise event.
		///     
		///     OnPropertyChanged(() => this.SomeProperty);
		/// </example>
		/// <param name="exp"></param>
		protected virtual void OnPropertyChanged<T>(Expression<Func<T>> exp)
		{
			if (PropertyChanged != null)
			{
				var member = exp.Body as MemberExpression; // if it is a member
				if (member != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(member.Member.Name));
					return;
				}

				var unary = exp.Body as UnaryExpression; // properties seems to trigger this?
				if (unary != null)
				{
					var prop = unary.Operand as MemberExpression;
					if (prop != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs(prop.Member.Name));
						return;
					}
				}
			}
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private bool _progressVisibility;
		public bool ProgressVisibility
		{
			get => _progressVisibility;
			set
			{
				_progressVisibility = value;
				OnPropertyChanged();
			}
		}

		#region Command Helpers

		/// <summary>
		/// Run a command if the updating flag is not set.
		/// If the flag is true(indicating the function is already running) then the action is not run.
		/// If the flag is false(indicating no running function) then the action is run.
		/// Once the action is finished if it was run, then the flag is reset to false once done.
		/// </summary>
		/// <param name="updatingFlag">The boolean property flag indicating if the command is already running</param>
		/// <param name="action">The action to run if command is not already running</param>
		/// <returns></returns>
		protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
		{
			if (updatingFlag.GetPropertyValue())
				return;

			updatingFlag.SetPropertyValue(true);

			try
			{
				await action();
			}
			finally
			{
				updatingFlag.SetPropertyValue(false);
			}
		}


		#endregion Command Helpers
	}
}
