using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.BackendApi.Models
{
	[Table("ProductImage")]
	public class ProductImage
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public bool IsThumbnail { get; set; }
		[Required]
		public string ImageUrl { get; set; }

		//Foreign
		public int ProductId { get; set; }
		public Product Product { get; set; }

	}
}

