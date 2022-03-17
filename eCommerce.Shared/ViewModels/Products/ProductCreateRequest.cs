using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Shared.ViewModels.Products
{
	public class ProductCreateRequest
	{
		[Required(ErrorMessage = "Please enter product name")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Please enter price of product")]
		public double Price { get; set; }
		public string? Description { get; set; }
		public IFormFile Image { get; set; }
		[Required(ErrorMessage = "Please enter category id")]
		public int CategoryId { get; set; }
		public int BrandId { get; set; }
	}
}

