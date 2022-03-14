using System;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.BackendApi.Models
{
	public class BrandCategory
	{
		[Key]
		public int Id { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }

		//Foreign
		public virtual Brand Brand { get; set; }

		public virtual Category Category { get; set; }
	}
}

