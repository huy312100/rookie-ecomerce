using System;
using System.ComponentModel.DataAnnotations;
using eCommerce.Shared.ViewModels.Products;

namespace eCommerce.Shared.ViewModels.Orders
{
	public class OrderDetailCreateRequest
	{
		[Required(ErrorMessage = "Please enter quantity")]
		public int Quantity { get; set; }
		public int OrderId { get; set; }
		[Required(ErrorMessage = "Please enter product id")]
		public int ProductId { get; set; }
	}
}

