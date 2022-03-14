using System;
using eCommerce.Shared.ViewModels.Orders;

namespace eCommerce.CustomerSite.ViewModels
{
	public class CheckoutVM
	{
		public CartVM Cart { get; set; }

		public CheckoutRequest CheckoutModel { get; set; }
	}
}

