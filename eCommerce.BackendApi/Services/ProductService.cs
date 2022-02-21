using System;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.BackendApi.Services
{
	public class ProductService :IProductService
	{
		private readonly ApplicationDbContext _dbContext;
		public async Task<List<Product>> GetAllProducts()
        {
			var data = await _dbContext.Products.ToListAsync();

			return data;
        }
	}
}

