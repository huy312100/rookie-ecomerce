using System;
using eCommerce.Shared.ViewModels.Products;

namespace eCommerce.CustomerSite.ViewModels
{
	public class CartItemVM
	{
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }

        public List<ProductImageVM>? Images { get; set; }

        public double Price { get; set; }

    }
}

