using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.BackendApi.Models.Enums.Order;

namespace eCommerce.BackendApi.Data.Models
{
	[Table("Order")]
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }

		public string? Note { get; set; }

		[Required]
		public OrderStatus Status { get; set; }

		[Required]
		public OrderPayment PaymentType { get; set; }

	}
}

