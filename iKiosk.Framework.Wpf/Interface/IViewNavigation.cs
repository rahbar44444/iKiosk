using iKiosk.Framework.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Framework.Wpf.Interface
{
	public interface IViewNavigation : INotifyPropertyChanged
	{
		ViewModelControlBase CurrentContent { get; }
		Stack<ViewModelControlBase> History { get; }
		ViewModelControlBase HomeVM { get; }
		bool IsReverseNavigation { get; }
		ViewModelControlBase PreviousContent { get; }
		Dictionary<Type, Type> ViewModelViewTypeMap { get; }

		void ClearHistory();
		void NavigateBack();
		void NavigateHome();
		void NavigateTo(ViewModelControlBase content, bool useTypeComparison);
		void NavigateTo<Tvm>(object message = null) where Tvm : ViewModelControlBase;
		void NavigateToShell<Tvm>(Tvm shellViewModel) where Tvm : ViewModelControlBase, new();
		ViewNavigation Register<Tvm, Tv>(params object[] viewModelPrameters)
			where Tvm : ViewModelControlBase
			where Tv : ViewControl;
		ViewNavigation RegisterHome<Tvm, Tv>(params object[] viewModelPrameters)
			where Tvm : ViewModelControlBase
			where Tv : ViewControl, new();
		object NavigateAndGetViewModel<Tvm>(object message = null) where Tvm : ViewModelControlBase;
		object NavigateBackAndGetViewModel();
		object NavigateToAndGetViewModel(ViewModelControlBase content, bool useTypeComparison);

	}
}
