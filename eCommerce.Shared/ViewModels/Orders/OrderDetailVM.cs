using System;
using eCommerce.Shared.ViewModels.Products;

namespace eCommerce.Shared.ViewModels.Orders
{
	public class OrderDetailVM
	{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public ProductVM Product { get; set; }
	}
}

