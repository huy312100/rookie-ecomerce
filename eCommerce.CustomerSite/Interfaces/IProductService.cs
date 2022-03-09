using System;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Products;

namespace eCommerce.CustomerSite.Interfaces
{
	public interface IProductService
	{
		Task<PagedResult<ProductVM>> GetTop8Product();
	}
}

