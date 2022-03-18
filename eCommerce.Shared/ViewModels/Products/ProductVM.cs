using System;
using eCommerce.Shared.ViewModels.Brands;
using eCommerce.Shared.ViewModels.Categories;
using eCommerce.Shared.ViewModels.Ratings;

namespace eCommerce.Shared.ViewModels.Products
{
	public class ProductVM
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public double Price { get; set; }
		public double? StarAverage { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public CategoryVM Category { get; set; }
		public BrandVM Brand { get; set; }
		public List<ProductImageVM>? Images { get; set; }
		//public List<RatingVM>? Ratings { get; set; }
	}
}

