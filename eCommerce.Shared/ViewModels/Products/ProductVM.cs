using System;
using eCommerce.Shared.ViewModels.Categories;

namespace eCommerce.Shared.ViewModels.Products
{
	public class ProductVM
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public double Price { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public CategoryVM? Category { get; set; }
		public List<ProductImageVM>? Images { get; set; }
	}
}

