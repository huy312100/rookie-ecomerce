using System;
using System.Threading.Tasks;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Models;
using eCommerce.BackendApi.Services;
using eCommerce.BackendApi.Storage;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace eCommerce.UnitTest.ServiceTests
{
	public class ProductServiceTest
	{
		private readonly ProductService _productService;

		public ProductServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(new Product
                {
                    Id = 1,
                    Name = "Iphone 13",
                    Price=25000000,
                    Description = "Lorem ipsum dolor sit amet",
                    CreatedDate = DateTime.Today,
                    UpdatedDate = null,
                    CategoryId = 1,
                    BrandId= 1,

                });
                context.Products.Add(new Product
                {
                    Id = 2,
                    Name = "Macbook M1",
                    Price = 45000000,
                    Description = "Lorem ipsum dolor sit amet",
                    CreatedDate = DateTime.Today,
                    UpdatedDate = null,
                    CategoryId = 2,
                    BrandId = 1,

                });
                context.Products.Add(new Product
                {
                    Id = 3,
                    Name = "Samsung S22 Ultra",
                    Price = 2000000,
                    Description = "Lorem ipsum dolor sit amet",
                    CreatedDate = DateTime.Today,
                    UpdatedDate = null,
                    CategoryId = 1,
                    BrandId = 2,

                });

                context.Users.Add(new User
                {
                    Id = new Guid("76251b87-c54d-47a1-bca2-df905d96c557"),
                    FirstName = "Huy",
                    LastName = "Nguyen",
                    UserName = "huy312100",
                    Dob= DateTime.Now,
                    Gender=0,
                    ImageUrl=null
                });
                context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum dolor sit amet",
                    ImageUrl = null
                });
                context.ProductImages.AddRange(
                    new ProductImage
                    {
                        Id = 1,
                        IsThumbnail = true,
                        ImageUrl = "https://rooter.lk/storage/phones/13-pro-max/13-pro-max-blue/iphone-13-pro-max-5g-smarttelefon-128gb-sierrabla-pdp-zoom-3000.jpeg",
                        ProductId = 1,
                    },
                    new ProductImage
                    {
                        Id = 2,
                        IsThumbnail = true,
                        ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcTjnCtC9nDsaxG---6YbKGe6i-K_5wEf_vUNk7EBTHn1KdRKNjw_Nj6TQDFsEIjzu3QoxpcWF_5pxQ&usqp=CAc",
                        ProductId = 1,
                    }
                 );

                context.Ratings.Add(new Rating
                {
                    Id = 1,
                    UserId = new Guid("76251b87-c54d-47a1-bca2-df905d96c557"),
                    Comment= "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae ",
                    CreatedDate= Convert.ToDateTime("2022-03-12 18:38:26.0000000"),
                    ProductId = 1,
                    Star = 5,
                });

                context.SaveChanges();

            }
            var mockContext = new ApplicationDbContext(options);
            var fileService = FileStorageService.IStorageService();
            _productService = new ProductService(mockContext, fileService);
        }

        [Fact]
        public async Task GetProductById_IdExists_ReturnAProductVM()
        {
            //Arrange
            int productId = 1;
            // Act 
            var result = await _productService.GetProductById(productId);
            // Assert
            Assert.IsType<ProductVM>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(2, result.Images.Count);
        }

        [Fact]
        public async Task GetProductById_IdNotFound_ThrowException()
        {
            //Arrange
            int productId = 99;
            // Act 
            var res = _productService.GetProductById(productId);
            // Assert
            Assert.Contains($"Cannot find product with ID {productId}", res.Exception.Message);
        }

        [Fact]
        public async Task GetProductPaging_PageIndexAndPageSizeAreEqual0_ReturnEmptyItem()
        {
            //Arrange
            var req = new PagingRequest()
            {
                PageIndex = 0,
                PageSize = 0
            };

            //Act
            var result = await _productService.GetProductPaging(req);

            //Arrange
            Assert.Empty(result.Items);
        }

        [Fact]
        public async Task GetProductByCategory_CategoryIdExistsAndPageSizeAndPageIndexEqual1_ReturnAPagedResultObjectAndProductItemsEqual1()
        {
            //Arrange
            int categoryId = 1;
            var req = new PagingRequest()
            {
                PageIndex = 1,
                PageSize = 1
            };
            // Act 
            var result = await _productService.GetProductByCategory(req,categoryId);
            // Assert
            Assert.IsType<PagedResult<ProductVM>>(result);
            Assert.Equal(categoryId, result.Items[0].Category.Id);
            Assert.Equal(1, result.Items.Count);
        }

        [Fact]
        public async Task GetProductByCategory_CategoryIdIsNotExist_ReturnEmptyItem()
        {
            //Arrange
            int categoryId = 99;
            var req = new PagingRequest()
            {
                PageIndex = 1,
                PageSize = 1
            };
            // Act 
            var result = await _productService.GetProductByCategory(req, categoryId);
            // Assert
            Assert.Empty(result.Items);
        }

        [Fact]
        public async Task CreateProduct_CategoryIsNotExist_ReturnException()
        {
            //Arrange
            var req = new ProductCreateRequest()
            {
                Name = "Example product",
                Price = 10,
                Description = "Lorem ipsum dolor sit amet",
                Image = null,
                CategoryId = 99
            };
            // Act 
            Func<Task> act = () => _productService.CreateProduct(req);
            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains($"Cannot create product because CategoryId {req.CategoryId} not found", exception.Message);
        }


    }
}

