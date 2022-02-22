using System;
using eCommerce.BackendApi.Models;
using eCommerce.Shared.ViewModels.Products;

namespace eCommerce.BackendApi.Interfaces
{
	public interface IProductService
	{
		Task<List<ProductVM>> GetAllProducts();
	}
}

