using iKiosk.Logic;
using iKiosk.Logic.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iKiosk.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class KioskController : ControllerBase
	{
		private readonly IKioskLogic _kioskLogic;
		public KioskController(IKioskLogic kioskLogic)
		{
			_kioskLogic = kioskLogic;
		}

		[HttpGet("languages")]
		public IActionResult GetAvailableLanguages()
		{
			var languages = _kioskLogic.GetAvailableLanguages();
			return Ok(languages);
		}

		[HttpPost("verify")]
		public IActionResult VerifyPersonalDetails([FromBody] PersonalDetailRequest request)
		{
			var response = _kioskLogic.VerifyPersonalDetails(request);

			return Ok(response);
		}

		[HttpGet("services")]
		public IActionResult GetAvailableServiceOptions()
		{
			var languages = _kioskLogic.GetAvailableServiceOptions();
			return Ok(languages);
		}

		[HttpPost("calculate-remittance")]
		public IActionResult Calculate([FromBody] RemittanceCalculationRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = _kioskLogic.CalculateRemittance(request);
			return Ok(result);
		}
	}
}
