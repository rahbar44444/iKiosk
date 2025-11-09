using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Logic.Model
{
	public class RemittanceCalculationResponse
	{
		public decimal ValueAddedTax { get; set; }
		public decimal AmountToPay { get; set; }
	}
}
