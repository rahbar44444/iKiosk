using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace iKiosk.UI.Services
{
	public class MessageService : IMessageService
	{
		public void Show(string message)
		{
			MessageBox.Show(message, "Message Service", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}
