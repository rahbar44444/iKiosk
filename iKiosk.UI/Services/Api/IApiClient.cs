using iKiosk.UI.Services.Models;

namespace iKiosk.UI.Services.Api
{
	public interface IApiClient
	{
		Task<ApiResult<IEnumerable<LanguageOption>>> GetLanguagesAsync();
		Task<ApiResult<PersonalDetailResponse>> VerifyPersonalDetailsAsync(PersonalDetailRequest request);
		Task<ApiResult<IEnumerable<ServiceOption>>> GetServicesAsync();
	}
}
