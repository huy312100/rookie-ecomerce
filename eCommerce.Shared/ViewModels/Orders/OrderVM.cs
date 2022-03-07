using System;
using eCommerce.Shared.Enums.Order;
namespace eCommerce.Shared.ViewModels.Orders
{
	public class OrderVM
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public string? Note { get; set; }
		public OrderStatus Status { get; set; }
		public OrderPayment PaymentType { get; set; }
		public List<OrderDetailVM> OrderDetails { get; set; }
	}
}

