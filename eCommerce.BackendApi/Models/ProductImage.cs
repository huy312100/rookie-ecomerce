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

	}
}

