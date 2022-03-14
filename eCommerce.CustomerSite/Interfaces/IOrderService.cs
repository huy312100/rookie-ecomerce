using System;
using eCommerce.Shared.ViewModels.Orders;

namespace eCommerce.CustomerSite.Interfaces
{
	public interface IOrderService
	{
		Task<bool> Checkout(CheckoutRequest request);
	}
}

