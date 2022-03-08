using System;
using eCommerce.Shared.ViewModels.Orders;

namespace eCommerce.BackendApi.Interfaces
{
	public interface IOrderService
	{
		Task<List<OrderVM>> GetAllOrders(Guid userId);
		Task<int> CheckoutOrder(CheckoutRequest req);
	}
}

