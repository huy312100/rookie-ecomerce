using System;
namespace eCommerce.CustomerSite.ViewModels
{
	public class CartVM
	{
		public List<CartItemVM> Items { get; set; }
		public double Total { get; set; }
	}
}

