﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.BackendApi.Models
{
	[Table("ProductImages")]
	public class ProductImage
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public bool IsThumbnail { get; set; }
		[Required]
		public string ImageUrl { get; set; }
		public int ProductId { get; set; }

		//Foreign
		public virtual Product Product { get; set; }

	}
}

