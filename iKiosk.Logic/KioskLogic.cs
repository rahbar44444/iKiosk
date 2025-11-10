using iKiosk.Logic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Logic
{
	public class KioskLogic : IKioskLogic
	{
		public IEnumerable<LanguageOption> GetAvailableLanguages()
		{
			return new List<LanguageOption>
					{
						new LanguageOption { Name = "العربية", CultureCode = "ar-SA" },   // Arabic (RTL)
					    new LanguageOption { Name = "English", CultureCode = "en-US" },   // English
					    new LanguageOption { Name = "اردو", CultureCode = "ur-PK" },       // Urdu
					    new LanguageOption { Name = "हिंदी", CultureCode = "hi-IN" },        // Hindi
					    new LanguageOption { Name = "മലയാളം", CultureCode = "ml-IN" },    // Malayalam
					    new LanguageOption { Name = "Filipino", CultureCode = "fil-PH" }, // Filipino
					    new LanguageOption { Name = "French", CultureCode = "fr-FR" },    // French
					    new LanguageOption { Name = "Spanish", CultureCode = "es-ES" },   // Spanish
					};
		}

		public PersonalDetailResponse VerifyPersonalDetails(PersonalDetailRequest request)
		{
			if (request.SaudiId < 1000000000 || request.SaudiId > 9999999999)
			{
				return new PersonalDetailResponse
				{
					IsValid = false,
					StatusMessage = "Saudi ID/Iqama ID is required."
				};
			}

			//  dummy data
			switch (request.SaudiId)
			{
				// Valid case – Mustafa Taj
				case 1234567890:
					return new PersonalDetailResponse
					{
						SaudiIqamaId = 1234567890,
						IsValid = true,
						FullName = "Mustafa Taj",
						DateOfBirth = "10/07/1995",
						StatusMessage = "Personal details verified successfully.",
						CustomerId = "CUST-1234567890",
						Nationality = "Saudi",
						IsExpired = false,
						AccountType = "Bank Account",
						BankName = "National Bank of Saudi Arabia",
						CountryCurrency = "Saudi Arabia - SAR"
					};

				// Expired case – Rahbar Khan
				case 1234567891:
					return new PersonalDetailResponse
					{
						SaudiIqamaId = 1234567891,
						IsValid = false,
						FullName = "Rahbar Khan",
						DateOfBirth = "10/07/1995",
						StatusMessage = "Your national ID/Iqama is expired. Please visit the nearest branch.",
						CustomerId = "CUST-9876543210",
						Nationality = "Indian",
						IsExpired = true
					};

				// Unknown user
				default:
					return new PersonalDetailResponse
					{
						IsValid = false,
						FullName = "Unknown",
						StatusMessage = "No record found for the provided ID.",
						CustomerId = string.Empty,
						Nationality = string.Empty,
						IsExpired = false
					};
			}
		}
		public IEnumerable<ServiceOption> GetAvailableServiceOptions()
		{
			return new List<ServiceOption>
					{
						new ServiceOption { Name = "Money Transfer" },
						new ServiceOption { Name = "Bill Payment" },
						new ServiceOption { Name = "Mobile Recharge" },
						new ServiceOption { Name = "Open Account" },
						new ServiceOption { Name = "Update ID" },
						new ServiceOption { Name = "Other Services" }
					};
		}

		public RemittanceCalculationResponse CalculateRemittance(RemittanceCalculationRequest request)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (request.ExchangeRate <= 0)
				throw new ArgumentException("Exchange rate must be greater than zero.");

			var vat = Math.Round(request.Fee * request.VatRate, 2, MidpointRounding.AwayFromZero);

			var amountInSar = Math.Round(request.AmountToSend / request.ExchangeRate, 2, MidpointRounding.AwayFromZero);

			var totalAmount = Math.Round(amountInSar + request.Fee + vat, 2, MidpointRounding.AwayFromZero);

			return new RemittanceCalculationResponse
			{
				ValueAddedTax = vat,
				AmountToPay = totalAmount
			};
		}


	}
}
