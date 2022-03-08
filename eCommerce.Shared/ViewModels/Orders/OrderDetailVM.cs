﻿using System;
using System.ComponentModel.DataAnnotations;
using eCommerce.Shared.ViewModels.Products;

namespace eCommerce.Shared.ViewModels.Orders
{
	public class OrderDetailVM
	{
		[Required]
		public int Quantity { get; set; }
		public ProductVM Product { get; set; }
	}
}

