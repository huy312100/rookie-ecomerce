using System;
using System.ComponentModel.DataAnnotations;
using eCommerce.Shared.Enums.Order;

namespace eCommerce.Shared.ViewModels.Orders
{
	public class CheckoutRequest
	{
		[Required(ErrorMessage = "Please enter User ID")]
		public Guid UserId { get; set; }
		[Required(ErrorMessage = "Please enter address")]
		public string Address { get; set; }
		[Required(ErrorMessage = "Please enter email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Please enter phone number")]
		public string PhoneNumber { get; set; }
		public string? Note { get; set; }
		[Required(ErrorMessage = "Please choose payment type")]
		public OrderPayment PaymentType { get; set; }
		public List<OrderDetailVM> OrderDetails { get; set; }
	}
}

