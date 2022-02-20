using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.BackendApi.Data.Models
{
	[Table("Product")]
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }

		public DateTime? UpdatedDate { get; set; }


	}
}

