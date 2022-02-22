using System;
namespace eCommerce.Shared.ViewModels.Products
{
	public class ProductImageVM
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string? ImageUrl { get; set; }
		public bool IsThumbnail { get; set; }
	}
}

