using iKiosk.Framework.Wpf.Interface;
using iKiosk.Framework.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Framework.Wpf
{
	public class ViewNavigation : ObservableObject, IViewNavigation
	{
		#region Variables

		// Private
		private bool isReverseNavigation;
		private ViewModelControlBase currentContentObject;
		private ViewModelControlBase previousContentObject;
		private Stack<ViewModelControlBase> history;

		#endregion Variables

		#region Properties

		/// <summary>
		/// Indicates whether the current navigation is in reverse direction
		/// </summary>
		public bool IsReverseNavigation
		{
			get
			{
				return isReverseNavigation;
			}
			private set
			{
				isReverseNavigation = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Holds Navigation history
		/// </summary>
		public Stack<ViewModelControlBase> History
		{
			get
			{
				return history;
			}
			private set
			{
				history = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Holds the current view model object
		/// </summary>
		public ViewModelControlBase CurrentContent
		{
			get
			{
				return currentContentObject;
			}
			private set
			{
				currentContentObject = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Holds the previous view model object
		/// </summary>
		public ViewModelControlBase PreviousContent
		{
			get
			{
				return previousContentObject;
			}
			private set
			{
				previousContentObject = value;
				OnPropertyChanged();
			}
		}
		#endregion Properties

		public ViewModelControlBase HomeVM { get; private set; }
		public Dictionary<Type, Type> ViewModelViewTypeMap { get; private set; }
		public Dictionary<Type, object[]> ViewModelParameterMap { get; private set; }

		#region Constructor

		public ViewNavigation()
		{
			History = new Stack<ViewModelControlBase>();
			ViewModelViewTypeMap = new Dictionary<Type, Type>();
			ViewModelParameterMap = new Dictionary<Type, object[]>();
		}
		#endregion Constructor

		#region Registration

		//public ViewNavigation Register<Tvm, Tv>(params object[] viewModelPrameters) where Tvm : ViewModelControlBase where Tv : ViewControl
		//{
		//	this.ViewModelViewTypeMap.Add(typeof(Tvm), typeof(Tv));
		//	this.ViewModelParameterMap.Add(typeof(Tvm), viewModelPrameters);

		//	return this;
		//}

		public ViewNavigation Register<Tvm, Tv>(params object[] viewModelPrameters)
	where Tvm : ViewModelControlBase
	where Tv : ViewControl
		{
			var vmType = typeof(Tvm);

			// ✅ Add or update View mapping safely
			if (!this.ViewModelViewTypeMap.ContainsKey(vmType))
				this.ViewModelViewTypeMap.Add(vmType, typeof(Tv));
			else
				this.ViewModelViewTypeMap[vmType] = typeof(Tv); // overwrite if needed

			// ✅ Add or update parameters safely
			if (!this.ViewModelParameterMap.ContainsKey(vmType))
				this.ViewModelParameterMap.Add(vmType, viewModelPrameters);
			else
				this.ViewModelParameterMap[vmType] = viewModelPrameters; // overwrite if needed

			return this;
		}


		public ViewNavigation RegisterHome<Tvm, Tv>(params object[] viewModelPrameters) where Tvm : ViewModelControlBase where Tv : ViewControl, new()
		{
			this.HomeVM = Activator.CreateInstance(typeof(Tvm), viewModelPrameters) as Tvm; ;
			this.HomeVM.WireUpView(new Tv());

			return this;
		}

		#endregion Registration

		#region Navigation

		/// <summary>
		/// Preforms the navigation between views
		/// </summary>
		/// <param name="content">Content object</param>

		public void NavigateTo<Tvm>(object message = null)
		where Tvm : ViewModelControlBase
		{
			// ToDo: leaky design - instances are created every navigation, but not disposed, should this be singleton pattern per V-VM pair or instances?
			var tv = this.ViewModelViewTypeMap[typeof(Tvm)];
			dynamic v = Activator.CreateInstance(tv) as ViewControl;
			var parammeters = this.ViewModelParameterMap[typeof(Tvm)];
			var vm = Activator.CreateInstance(typeof(Tvm), parammeters) as Tvm;
			vm.WireUpView(v);
			vm.ViewModelLoaded(message);
			this.NavigateTo(vm, false);
		}
		public object NavigateAndGetViewModel<Tvm>(object message = null)
		where Tvm : ViewModelControlBase
		{
			// ToDo: leaky design - instances are created every navigation, but not disposed, should this be singleton pattern per V-VM pair or instances?
			var tv = this.ViewModelViewTypeMap[typeof(Tvm)];
			dynamic v = Activator.CreateInstance(tv) as ViewControl;
			var parammeters = this.ViewModelParameterMap[typeof(Tvm)];
			var vm = Activator.CreateInstance(typeof(Tvm), parammeters) as Tvm;
			vm.WireUpView(v);
			vm.ViewModelLoaded(message);
			this.NavigateTo(vm, false);
			return vm;
		}

		public void NavigateToShell<Tvm>(Tvm shellViewModel)
		where Tvm : ViewModelControlBase, new()
		{
			// ToDo: leaky design - instances are created every navigation, but not disposed, should this be singleton pattern per V-VM pair or instances?
			var tv = this.ViewModelViewTypeMap[typeof(Tvm)];
			dynamic v = Activator.CreateInstance(tv) as ViewControl;
			var vm = new Tvm();
			vm = shellViewModel;
			vm.WireUpView(v);
			this.NavigateTo(vm, false);
		}

		/// <summary>
		/// Preforms the navigation between views
		/// </summary>
		/// <param name="content">View model object to be navigated</param>
		public void NavigateTo(ViewModelControlBase content, bool useTypeComparison)
		{
			if (content == null)
			{
				IsReverseNavigation = true; // content null means reset history, effectively navigate back to starting point hence IsReverseNavigation = true
				CurrentContent = null;
			}
			else
			{
				if (useTypeComparison)
				{
					if (CurrentContent != null && content.GetType() == CurrentContent.GetType())
					{
						return; // Navigate to same type as current object, do nothing
					}
				}
				else if (content == CurrentContent)
				{
					return; // Navigate to current instance, do nothing
				}

				if (History.Contains(content))
				{
					// Traverse history in reverse to instance.
					IsReverseNavigation = true;

					while (History.Peek() != content)
					{
						History.Pop();
					}

					History.Pop();
				}
				else
				{
					// Add new instance to navigation history
					IsReverseNavigation = false;

					if (CurrentContent != null)
					{
						History.Push(CurrentContent);
					}
				}

				PreviousContent = CurrentContent;
				CurrentContent = content;
			}
		}

		/// <summary>
		/// Navigate to the first view in the history
		/// </summary>
		/// <returns></returns>
		public void NavigateHome()
		{
			this.NavigateTo(this.HomeVM, true);
		}

		/// <summary>
		/// Clears the navigatio history
		/// </summary>
		public void ClearHistory()
		{
			History.Clear();
		}

		/// <summary>
		/// Goto previous view
		/// </summary>
		public void NavigateBack()
		{
			NavigateTo(History.Peek(), false);
		}

		public object NavigateBackAndGetViewModel()
		{
			return NavigateToAndGetViewModel(History.Peek(), false);
		}
		public object NavigateToAndGetViewModel(ViewModelControlBase content, bool useTypeComparison)
		{
			if (content == null)
			{
				IsReverseNavigation = true; // content null means reset history, effectively navigate back to starting point hence IsReverseNavigation = true
				CurrentContent = null;
			}
			else
			{
				if (useTypeComparison)
				{
					if (CurrentContent != null && content.GetType() == CurrentContent.GetType())
					{
						return default; // Navigate to same type as current object, do nothing
					}
				}
				else if (content == CurrentContent)
				{
					return default; // Navigate to current instance, do nothing
				}

				if (History.Contains(content))
				{
					// Traverse history in reverse to instance.
					IsReverseNavigation = true;

					while (History.Peek() != content)
					{
						History.Pop();
					}

					History.Pop();
				}
				else
				{
					// Add new instance to navigation history
					IsReverseNavigation = false;

					if (CurrentContent != null)
					{
						History.Push(CurrentContent);
					}
				}

				PreviousContent = CurrentContent;
				CurrentContent = content;
			}
			return CurrentContent;
		}

		#endregion Navigation
	}
}
