using System;
using eCommerce.BackendApi.Models;

namespace eCommerce.BackendApi.Interfaces
{
	public interface IProductService
	{
		Task<List<Product>> GetAllProducts();
	}
}

