using System;
using eCommerce.Shared.ViewModels.Orders;

namespace eCommerce.CustomerSite.Interfaces
{
	public interface ICartService
	{
		Task<bool> Checkout(CheckoutRequest request);
	}
}

