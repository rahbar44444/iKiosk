using iKiosk.Framework.Wpf.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Framework.Wpf.ViewModel
{
	public abstract class ViewModelBase<T> : ObservableObject where T : IView
	{
		public T View { get; protected set; }
	}
}
