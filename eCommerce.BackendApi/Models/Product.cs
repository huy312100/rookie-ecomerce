using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.BackendApi.Models;

namespace eCommerce.BackendApi.Models
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

		//Foreign 
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public List<ProductImage> ProductImages { get; set; }

		public List<Rating> Ratings { get; set; }

		public ICollection<Order> Orders { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }
	}
}

