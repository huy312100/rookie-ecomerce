using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.Shared.Enums.Order;

namespace eCommerce.BackendApi.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime OrderDate { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		public string? Note { get; set; }
		[Required]
		public OrderStatus Status { get; set; }
		[Required]
		public OrderPayment PaymentType { get; set; }
		public double Total { get; set; }
		public Guid UserId { get; set; }

		//Foreign
		public virtual List<OrderDetail> OrderDetails { get; set; }
		public virtual User User { get; set; }
	}
}

