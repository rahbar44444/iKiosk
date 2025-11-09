using iKiosk.UI.Services.Models;
using iKiosk.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.UI.Services.Api
{
	public class ApiClient : IApiClient
	{
		private readonly HttpClient _http;

		public ApiClient(HttpClient http)
		{
			_http = http;
		}

		private async Task<ApiResult<T>> TryAsync<T>(Func<Task<T>> func)
		{
			try
			{
				return new ApiResult<T>(await func());
			}
			catch (HttpRequestException ex)
			{
				return new ApiResult<T>(default, false, $"Network error: {ex.Message}");
			}
			catch (Exception ex)
			{
				return new ApiResult<T>(default, false, ex.Message);
			}
		}

		public Task<ApiResult<IEnumerable<LanguageOption>>> GetLanguagesAsync()
		{
			return TryAsync(async () =>
			{
				var result = await _http.GetFromJsonAsync<IEnumerable<LanguageOption>>(EndpointRoutes.Languages);
				return result ?? Enumerable.Empty<LanguageOption>();
			});
		}

		public Task<ApiResult<PersonalDetailResponse>> VerifyPersonalDetailsAsync(PersonalDetailRequest request)
		{
			return TryAsync(async () =>
			{
				var response = await _http.PostAsJsonAsync(EndpointRoutes.VerifyPersonalDetails, request);
				response.EnsureSuccessStatusCode();

				var data = await response.Content.ReadFromJsonAsync<PersonalDetailResponse>();
				return data!;
			});
		}

		public Task<ApiResult<IEnumerable<ServiceOption>>> GetServicesAsync()
		{
			return TryAsync(async () =>
			{
				var result = await _http.GetFromJsonAsync<IEnumerable<ServiceOption>>(EndpointRoutes.Services);
				return result ?? Enumerable.Empty<ServiceOption>();
			});
		}
	}
}
