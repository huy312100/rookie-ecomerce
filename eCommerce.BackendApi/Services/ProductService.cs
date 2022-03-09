using System;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using Microsoft.EntityFrameworkCore;
using eCommerce.Shared.ViewModels.Products;
using eCommerce.Shared.ViewModels.Categories;
using System.Net.Http.Headers;
using eCommerce.Shared.ViewModels.Common;

namespace eCommerce.BackendApi.Services
{
	public class ProductService : IProductService
	{
		private readonly ApplicationDbContext _dbContext;
        private readonly IFileStorageService _fileStorageService;

        public ProductService(ApplicationDbContext dbContext,IFileStorageService fileStorageService)
        {
			_dbContext = dbContext;
            _fileStorageService = fileStorageService;
        }

		//public async Task<List<ProductVM>> GetAllProducts()
		//{
		//	var query = from p in _dbContext.Products
		//				join c in _dbContext.Categories
		//				on p.CategoryId equals c.Id
		//				join pi in _dbContext.ProductImages
		//				on p.Id equals pi.ProductId
		//				select new { p, c, pi };
		//	var data = await query.Select(res => new ProductVM()
		//	{
		//		Id = res.p.Id,
		//		Name = res.p.Name,
  //              Description=res.p.Description,
		//		Price = res.p.Price,
		//		CreatedDate = res.p.CreatedDate,
		//		UpdatedDate = res.p.UpdatedDate,
		//		Category = new CategoryVM
		//		{
		//			Id = res.c.Id,
		//			Name = res.c.Name
		//		},
  //              Images = query.Select(data => new ProductImageVM()
  //              {
  //                  Id = data.pi.Id,
  //                  ImageUrl = data.pi.ImageUrl,
  //                  IsThumbnail = data.pi.IsThumbnail
  //              }).ToList()
  //          }).ToListAsync();
		//	return data;
		//}

        public async Task<PagedResult<ProductVM>> GetProductPaging(PagingRequest req)
        {
            //1. join product and category
            var query = from p in _dbContext.Products
                        join c in _dbContext.Categories
                        on p.CategoryId equals c.Id
                        join pi in _dbContext.ProductImages
                        on p.Id equals pi.ProductId
                        select new { p, c, pi };

            //3.Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize)
                .Select(prop => new ProductVM()
                {
                    Id = prop.p.Id,
                    Name = prop.p.Name,
                    Description = prop.p.Description,
                    Price = prop.p.Price,
                    CreatedDate = prop.p.CreatedDate,
                    UpdatedDate = prop.p.UpdatedDate,
                    Category = new CategoryVM
                    {
                        Id = prop.c.Id,
                        Name = prop.c.Name
                    },
                    Images = query.Select(data => new ProductImageVM()
                    {
                        Id = data.pi.Id,
                        ImageUrl = data.pi.ImageUrl,
                        IsThumbnail = data.pi.IsThumbnail
                    }).ToList()
                }).ToListAsync();

            var pagedResult = new PagedResult<ProductVM>()
            {
                TotalRecords = totalRow,
                PageSize = req.PageSize,
                PageIndex = req.PageIndex,
                Items = data
            };
            return pagedResult;
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

            var category = await _dbContext.Categories.FindAsync(product.CategoryId);

            var productViewModel = new ProductVM()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description=product.Description,
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

        public async Task<List<ProductVM>> GetProductByCategory(PagingRequest req,int categoryId)
        {
            var query = from p in _dbContext.Products
                        join c in _dbContext.Categories
                        on p.CategoryId equals c.Id
                        join pi in _dbContext.ProductImages
                        on p.Id equals pi.ProductId
                        where c.Id == categoryId
                        select new { p, c, pi };

            int totalRow = await query.CountAsync();

            var data = await query.Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize)
            .Select(res => new ProductVM()
             {
                Id = res.p.Id,
                Name = res.p.Name,
                Description=res.p.Description,
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

        //CUD
        public async Task<int> CreateProduct(ProductCreateRequest req)
        {
            //check if category is existed or not
            var categoryId = await _dbContext.Categories.FindAsync(req.CategoryId);

            if (categoryId == null)
            {
                throw new Exception($"Cannot create product because CategoryId {req.CategoryId} not found");
            }

            var product = new Product()
            {
                Name = req.Name,
                Description=req.Description,
                Price = req.Price,
                CreatedDate = DateTime.Now,
                CategoryId = req.CategoryId
            };

            if (req.Image != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        ImageUrl= await _fileStorageService.SaveFile(req.Image),
                        IsThumbnail=false
                    }
                };
            }
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> DeleteProduct(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);

            if(product == null)
            {
                throw new Exception($"Cannot delete product because ProductId not found");
            }

            var image = _dbContext.ProductImages.Where(data => data.ProductId == productId);

            foreach(var item in image)
            {
                await _fileStorageService.DeleteFileAsync(item.ImageUrl);
            }

            _dbContext.Products.Remove(product);
            return await _dbContext.SaveChangesAsync();
        }
    }
}

