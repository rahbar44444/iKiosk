using iKiosk.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Logic
{
	public interface IKioskLogic
	{
		IEnumerable<Logic.Model.LanguageOption> GetAvailableLanguages();
		Logic.Model.PersonalDetailResponse VerifyPersonalDetails(Logic.Model.PersonalDetailRequest request);
		IEnumerable<Logic.Model.ServiceOption> GetAvailableServiceOptions();
		RemittanceCalculationResponse CalculateRemittance(RemittanceCalculationRequest request);
	}
}
