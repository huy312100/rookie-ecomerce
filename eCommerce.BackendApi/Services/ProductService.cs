using System;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using Microsoft.EntityFrameworkCore;
using eCommerce.Shared.ViewModels.Products;
using eCommerce.Shared.ViewModels.Categories;

namespace eCommerce.BackendApi.Services
{
	public class ProductService :IProductService
	{
		private readonly ApplicationDbContext _dbContext;
		public ProductService(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
        }

		public async Task<List<ProductVM>> GetAllProducts()
		{
			var query = from p in _dbContext.Products
						join c in _dbContext.Categories
						on p.CategoryId equals c.Id
						join pi in _dbContext.ProductImages
						on p.Id equals pi.ProductId
						select new { p, c, pi };
			var data = await query.Select(res => new ProductVM
			{
				Id = res.p.Id,
				Name = res.p.Name,
				Price = res.p.Price,
				CreatedDate = res.p.CreatedDate,
				UpdatedDate = res.p.UpdatedDate,
				Category = new CategoryVM
				{
					Id = res.c.Id,
					Name = res.c.Name
				}


			}).ToListAsync();
			return data;
		}

		//public async Task<ProductVM>GetProductById(int id)
  //      {
		//	var product = await _dbContext.Products.FindAsync(id);

		//	if(product == null)
  //          {
		//		throw new Exception($"Cannot find product with ID {id}");
  //          }

		//	var images = await _dbContext.ProductImages.Where(img => img.ProductId == id)
		//				.ToListAsync();

		//	return product;
		//}

	}
}

