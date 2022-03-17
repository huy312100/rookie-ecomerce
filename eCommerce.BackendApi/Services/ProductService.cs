using System;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using Microsoft.EntityFrameworkCore;
using eCommerce.Shared.ViewModels.Products;
using eCommerce.Shared.ViewModels.Categories;
using System.Net.Http.Headers;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Ratings;
using eCommerce.Shared.ViewModels.Brands;

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

        public async Task<List<ProductVM>> GetAllProducts()
        {
            var query = from p in _dbContext.Products
                        join b in _dbContext.Brands
                        on p.BrandId equals b.Id
                        join c in _dbContext.Categories
                        on p.CategoryId equals c.Id
                        join pi in _dbContext.ProductImages
                        on p.Id equals pi.ProductId into obj1
                        from pi in obj1.DefaultIfEmpty()
                        select new { p, c, pi, b };

            var data = await query.Select(prop => new ProductVM()
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
                Brand = new BrandVM
                {
                    Id = prop.b.Id,
                    Name = prop.b.Name,
                    Description = prop.b.Description
                },

                Images = query.Where(x => prop.pi.ProductId == x.p.Id && x.pi.IsThumbnail == true)
                    .Select(data => new ProductImageVM()
                    {
                        Id = data.pi.Id,
                        ImageUrl = data.pi.ImageUrl,
                        IsThumbnail = data.pi.IsThumbnail
                    }).ToList()
            }).ToListAsync();

            var uniqueItem = data.GroupBy(x => x.Id).Select(x => x.First()).ToList();

            return uniqueItem;
        }

        public async Task<PagedResult<ProductVM>> GetProductPaging(PagingRequest req)
        {
            //1. join product and category
            var query = from p in _dbContext.Products
                        join b in _dbContext.Brands
                        on p.BrandId equals b.Id
                        join c in _dbContext.Categories
                        on p.CategoryId equals c.Id
                        join pi in _dbContext.ProductImages
                        on p.Id equals pi.ProductId into obj1
                        from pi in obj1.DefaultIfEmpty()
                        join r in _dbContext.Ratings
                        on p.Id equals r.ProductId into obj2
                        from r in obj2.DefaultIfEmpty()
                        select new { p, c, pi, r, b };

            var averageStar = await query.Select(prop => prop.r.Star).AverageAsync();

            //3.Paging
            //int totalRow = await query.CountAsync();

            var data = await query.Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize)
                //.GroupBy(x=>x.p.Id)
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
                    Brand = new BrandVM
                    {
                        Id = prop.b.Id,
                        Name = prop.b.Name,
                        Description = prop.b.Description
                    },
                    StarAverage = averageStar,

                    Images = query.Where(x => prop.pi.ProductId == x.p.Id)
                        .Select(data => new ProductImageVM()
                        {
                            Id = data.pi.Id,
                            ImageUrl = data.pi.ImageUrl,
                            IsThumbnail = data.pi.IsThumbnail
                        }).ToList()
                }).ToListAsync();

            var uniqueItem = data.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            int totalRow = uniqueItem.Count();

            var pagedResult = new PagedResult<ProductVM>()
            {
                TotalRecords = totalRow,
                PageSize = req.PageSize,
                PageIndex = req.PageIndex,
                Items = uniqueItem
            };
            return pagedResult;
        }

        public async Task<ProductVM> GetProductById(int id)
        {

            var query = from p in _dbContext.Products
                        join b in _dbContext.Brands
                        on p.BrandId equals b.Id
                        join c in _dbContext.Categories
                        on p.CategoryId equals c.Id
                        join pi in _dbContext.ProductImages
                        on p.Id equals pi.ProductId into obj1
                        from pi in obj1.DefaultIfEmpty()
                        join r in _dbContext.Ratings
                        on p.Id equals r.ProductId into obj2
                        from r in obj2.DefaultIfEmpty()
                        where p.Id == id
                        select new { p, c, pi, r, b };

            var product = await query.FirstOrDefaultAsync();
            var averageStar = await query.Select(prop => prop.r.Star).AverageAsync();

            if (product == null)
            {
                throw new Exception($"Cannot find product with ID {id}");
            }

            var images = await _dbContext.ProductImages.Where(prop => prop.ProductId == id)
                            .ToListAsync();

            var productViewModel = new ProductVM()
            {
                Id = product.p.Id,
                Name = product.p.Name,
                Price = product.p.Price,
                Description=product.p.Description,
                CreatedDate = product.p.CreatedDate,
                UpdatedDate = product.p.UpdatedDate,
                Category = new CategoryVM
                {
                    Id = product.c.Id,
                    Name = product.c.Name,
                    Description=product.c.Description,
                    ImageUrl=product.c.ImageUrl,
                    CreatedDate=product.c.CreatedDate,
                    ParentId=product.c.ParentId
                },
                Brand = new BrandVM
                {
                    Id = product.b.Id,
                    Name = product.b.Name,
                    Description = product.b.Description
                },
                StarAverage = averageStar,
                Images = images.Select(data => new ProductImageVM()
                {
                    Id = data.Id,
                    ImageUrl = data.ImageUrl,
                    IsThumbnail=data.IsThumbnail
                }).ToList()
            };
            return productViewModel;
        }

        public async Task<PagedResult<ProductVM>> GetProductByCategory(PagingRequest req,int categoryId)
        {
            var query = from p in _dbContext.Products
                        join b in _dbContext.Brands
                        on p.BrandId equals b.Id
                        join c in _dbContext.Categories
                        on p.CategoryId equals c.Id
                        join pi in _dbContext.ProductImages
                        on p.Id equals pi.ProductId into obj1
                        from pi in obj1.DefaultIfEmpty()
                        join r in _dbContext.Ratings
                        on p.Id equals r.ProductId into obj2
                        from r in obj2.DefaultIfEmpty()
                        where c.Id == categoryId
                        select new { p, c, pi, r, b };

            var averageStar = await query.Select(prop => prop.r.Star).AverageAsync();

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
                    Brand = new BrandVM
                    {
                        Id= prop.b.Id,
                        Name=prop.b.Name,
                        Description=prop.b.Description
                    },
                    StarAverage = (double)prop.pi.Id,

                    Images = query.Where(x => prop.pi.ProductId == x.p.Id)
                        .Select(data => new ProductImageVM()
                        {
                            Id = data.pi.Id,
                            ImageUrl = data.pi.ImageUrl,
                            IsThumbnail = data.pi.IsThumbnail
                        }).ToList()
                }).ToListAsync();

            var uniqueItem = data.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            int totalRow = uniqueItem.Count();

            var pagedResult = new PagedResult<ProductVM>()
            {
                TotalRecords = totalRow,
                PageSize = req.PageSize,
                PageIndex = req.PageIndex,
                Items = uniqueItem
            };
            return pagedResult;
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
                CategoryId = req.CategoryId,
                BrandId=req.BrandId
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

        public async Task<int> UpdateProduct(ProductUpdateRequest req)
        {
            //check if category is existed or not
            var product = await _dbContext.Products.FindAsync(req.Id);

            if (product == null)
            {
                throw new Exception($"Cannot update product because ProductId {req.Id} is null or not found");
            }

            product.Name = req.Name;
            product.Description = req.Description;
            product.Price = req.Price;
            product.UpdatedDate = DateTime.Now;
            product.CategoryId = req.CategoryId;
            product.BrandId = req.BrandId;

            if (req.Image != null)
            {
                var image = await _dbContext.ProductImages.FirstOrDefaultAsync(i => i.IsThumbnail == true && i.ProductId == req.Id);
                if (image != null)
                {
                    image.ImageUrl = await _fileStorageService.SaveFile(req.Image);
                    _dbContext.ProductImages.Update(image);
                }

            }

            _dbContext.Products.Update(product);
            return await _dbContext.SaveChangesAsync();
        }
    }
}

