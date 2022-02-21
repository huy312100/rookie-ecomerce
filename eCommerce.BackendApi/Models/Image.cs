using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.BackendApi.Models
{
	[Table("Image")]
	public class Image
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string ImageUrl { get; set; }

		public ICollection<Product> Products { get; set; }
		public List<ProductImage> ProductImages { get; set; }
	}
}

