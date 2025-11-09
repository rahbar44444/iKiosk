using iKiosk.Framework.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.UI.Services.Models
{
	public class ServiceOption:ViewModelControlBase
	{
		public string Name { get; set; }
		private bool _isSelected;

		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				if (_isSelected != value)
				{
					_isSelected = value;
					OnPropertyChanged(nameof(IsSelected));
				}
			}
		}
	}
}
