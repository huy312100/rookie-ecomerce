using System;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Shared.ViewModels.Products
{
	public class ProductUpdateRequest
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public string Description { get; set; }
		public IFormFile? Image { get; set; }
		public int CategoryId { get; set; }
		public int BrandId { get; set; }
	}
}

