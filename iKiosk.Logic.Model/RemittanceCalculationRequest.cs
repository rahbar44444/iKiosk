using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Logic.Model
{
	public class RemittanceCalculationRequest
	{
		public decimal AmountToSend { get; set; }
		public decimal ExchangeRate { get; set; }
		public decimal Fee { get; set; }
		public decimal VatRate { get; set; } = 0.15m; 
	}
}
