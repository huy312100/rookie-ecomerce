using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.BackendApi.Data.Models;

namespace eCommerce.BackendApi.Models
{
	[Table("OrderDetail")]
	public class OrderDetail
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int Quantity { get; set; }

		//Foreign
		public int OrderId { get; set; }
		public Order Order { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}

