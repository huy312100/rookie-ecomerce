using System;
using eCommerce.Shared.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.CustomerSite.Controllers.Components
{
	public class PagerViewComponent : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync(PagedResultBase list)
		{
			return Task.FromResult((IViewComponentResult)View("Default", list));
		}
	}
}

