using System;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Products;

namespace eCommerce.CustomerSite.ViewModels
{
	public class ProductCategoryVM
	{
		public PagedResult<ProductVM> ProductByCategory { get; set; }
	}
}

