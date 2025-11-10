using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Logic.Model
{
	public class PersonalDetailResponse
	{
		public int SaudiIqamaId { get; set; }
		public bool IsValid { get; set; }
		public string FullName { get; set; } = string.Empty;
		public string DateOfBirth { get; set; } = string.Empty;
		public string StatusMessage { get; set; } = string.Empty;
		public string CustomerId { get; set; } = string.Empty;
		public string Nationality { get; set; } = string.Empty;
		public string CountryCurrency { get; set; } = string.Empty;
		public string AccountType { get; set; } = string.Empty;
		public string BankName { get; set; } = string.Empty;
		public bool IsExpired { get; set; }
	}
}
