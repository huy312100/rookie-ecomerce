using System;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.BackendApi.Models
{
	public class Brand
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }

		//Foreign key
		public virtual List<BrandCategory> BrandCategories { get; set; }

		public virtual List<Product> Products { get; set; }

	}
}

