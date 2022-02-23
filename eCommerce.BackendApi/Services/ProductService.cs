using System;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using Microsoft.EntityFrameworkCore;
using eCommerce.Shared.ViewModels.Products;
using eCommerce.Shared.ViewModels.Categories;

namespace eCommerce.BackendApi.Services
{
	public class ProductService : IProductService
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
			var data = await query.Select(res => new ProductVM()
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
				},
                Images = query.Select(data => new ProductImageVM()
                {
                    Id = data.pi.Id,
                    ImageUrl = data.pi.ImageUrl,
                    IsThumbnail = data.pi.IsThumbnail
                }).ToList()
            }).ToListAsync();
			return data;
		}

        public async Task<ProductVM> GetProductById(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                throw new Exception($"Cannot find product with ID {id}");
            }

            var images = await _dbContext.ProductImages.Where(prop => prop.ProductId == id)
                            .ToListAsync();

            //var thumbnailImg = images.SingleOrDefault(prop => prop.IsThumbnail == true);
            var category = await _dbContext.Categories.FindAsync(product.CategoryId);

            //var ratingCount = _dbContext.Ratings.Where(x => x.ProductId == productId).Count();

            var productViewModel = new ProductVM()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate,
                Category = new CategoryVM
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description=category.Description,
                    ImageUrl=category.ImageUrl,
                    CreatedDate=category.CreatedDate,
                    ParentId=category.ParentId
                },
                Images = images.Select(data => new ProductImageVM()
                {
                    Id = data.Id,
                    ImageUrl = data.ImageUrl,
                    IsThumbnail=data.IsThumbnail
                }).ToList()
            };
            return productViewModel;
        }

        public async Task<List<ProductVM>> GetProductByCategory(int categoryId)
        {
            var query = from p in _dbContext.Products
                        join c in _dbContext.Categories
                        on p.CategoryId equals c.Id
                        join pi in _dbContext.ProductImages
                        on p.Id equals pi.ProductId
                        where c.Id == categoryId
                        select new { p, c, pi };

            var data = await query.Select(res => new ProductVM()
            {
                Id = res.p.Id,
                Name = res.p.Name,
                Price = res.p.Price,
                CreatedDate = res.p.CreatedDate,
                UpdatedDate = res.p.UpdatedDate,
                Category = new CategoryVM
                {
                    Id = res.c.Id,
                    Name = res.c.Name,
                    Description = res.c.Description,
                    ImageUrl = res.c.ImageUrl,
                    CreatedDate = res.c.CreatedDate,
                    ParentId = res.c.ParentId
                },
                Images = query.Select(data => new ProductImageVM()
                {
                    Id = data.pi.Id,
                    ImageUrl = data.pi.ImageUrl,
                    IsThumbnail = data.pi.IsThumbnail
                }).ToList()
            }).ToListAsync();

            return data;
        }

        public async Task<ProductImageVM> GetImageById(int id)
        {
            var image = await _dbContext.ProductImages.FindAsync(id);

            if(image == null)
            {
                throw new Exception($"Cannot find image with ID {id}");
            }

            var imageViewModel = new ProductImageVM()
            {
                Id = image.Id,
                ProductId = image.ProductId,
                ImageUrl = image.ImageUrl,
                IsThumbnail = image.IsThumbnail
            };

            return imageViewModel;
        }

        //public async Task<ProductVM> CreateProduct(ProductCreateRequest req)
        //{
        //    //check if category is existed or not
        //    var categoryId = await _dbContext.Categories.FindAsync(req.CategoryId);

        //    if(categoryId == null)
        //    {
        //        throw new Exception($"Cannot create product because CategoryId {cateforyId} not found");
        //    }

        //    var product = new Product()
        //    {
        //        Name = req.Name,
        //        Price=req.Price,
        //        CreatedDate=DateTime.Now,
        //        CategoryId=req.CategoryId
        //    }
        //    if(req.Image != null) {
        //        product.ProductImages = new List<ProductImage>()
        //        {
        //            new ProductImage()
        //            {
        //                ImageUrl=;
        //            }
        //        }
        //    }
        //}
    }
}

