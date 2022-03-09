using System;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Orders;

namespace eCommerce.BackendApi.Interfaces
{
	public interface IOrderService
	{
		Task<List<OrderVM>> GetOrdersPaging(PagingRequest req, Guid userId);
		Task<int> CheckoutOrder(CheckoutRequest req);
	}
}

