using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.BackendApi.Models
{
	public class OrderDetail
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int Quantity { get; set; }
		public int OrderId { get; set; }
		[Required(ErrorMessage ="Please enter product id")]
		public int ProductId { get; set; }
		public double SubTotal { get; set; }
		//Foreign
		public virtual Order Order { get; set; }

		public virtual Product Product { get; set; }
	}
}

