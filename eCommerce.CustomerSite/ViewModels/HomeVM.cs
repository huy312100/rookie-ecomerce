using System;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Products;

namespace eCommerce.CustomerSite.ViewModels
{
	public class HomeVM
	{
        public PagedResult<ProductVM> FeaturedProducts { get; set; }
	}
}

