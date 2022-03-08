using System;
using eCommerce.Shared.Enums.Order;
namespace eCommerce.Shared.ViewModels.Orders
{
	public class OrderVM
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string? Note { get; set; }
		public OrderStatus Status { get; set; }
		public OrderPayment PaymentType { get; set; }
		public double Total { get; set; }
		public List<OrderDetailVM> OrderDetails { get; set; }= new List<OrderDetailVM>();
	}
}

