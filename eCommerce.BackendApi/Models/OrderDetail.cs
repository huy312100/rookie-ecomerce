using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.BackendApi.Models
{
	[Table("OrderDetail")]
	public class OrderDetail
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}

