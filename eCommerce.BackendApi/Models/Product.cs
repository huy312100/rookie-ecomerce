using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.BackendApi.Models;

namespace eCommerce.BackendApi.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public double Price { get; set; }
		public string? Description { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int CategoryId { get; set; }
		public int BrandId { get; set; }

		//Foreign 
		public virtual Category Category { get; set; }

		public virtual Brand Brand { get; set; }

		public virtual List<ProductImage> ProductImages { get; set; }

		public virtual List<Rating> Ratings { get; set; }

		public virtual List<OrderDetail> OrderDetails { get; set; }
	}
}

