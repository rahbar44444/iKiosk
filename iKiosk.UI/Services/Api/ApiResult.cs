using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.UI.Services.Api
{
	public record ApiResult<T>(T? Data, bool IsSuccess = true, string? ErrorMessage = null)
	{
		public bool HasError => !IsSuccess || Data == null;
	}

}
