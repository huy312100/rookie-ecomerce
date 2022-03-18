using System;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Products;
using eCommerce.Shared.ViewModels.Ratings;

namespace eCommerce.CustomerSite.ViewModels
{
	public class ProductDetailVM
	{
		public PagedResult<RatingVM> ProductRatings { get; set; }
		public ProductVM Product { get; set; }
	}
}

