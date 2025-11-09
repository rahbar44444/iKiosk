using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Framework.Wpf.ViewModel
{
	public class ViewModelControlBase : ViewModelBase<ViewControl>
	{
		public virtual void WireUpView<Tv>(Tv v) where Tv : ViewControl
		{
			v.DataContext = this;
			this.View = v;
		}
		public virtual void ViewModelLoaded(object message)
		{
		}
	}
}
