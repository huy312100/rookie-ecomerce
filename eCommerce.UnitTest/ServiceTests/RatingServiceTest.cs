using System;
using System.Threading.Tasks;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Models;
using eCommerce.BackendApi.Services;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Ratings;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace eCommerce.UnitTest.ServiceTests
{
	public class RatingServiceTest
	{
        private readonly RatingService _ratingService;

        public RatingServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Brands.Add(new Brand
                {
                    Id = 1,
                    Name = "Apple",
                    Description = "Lorem ipsum dolor sit amet",
                });
                context.Products.Add(new Product
                {
                    Id = 1,
                    Name = "Iphone 13",
                    Price = 25000000,
                    Description = "Lorem ipsum dolor sit amet",
                    CreatedDate = DateTime.Today,
                    UpdatedDate = null,
                    CategoryId = 1,
                    BrandId = 1,

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
                    Dob = DateTime.Now,
                    Gender = 0,
                    ImageUrl = null
                });
                context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum dolor sit amet",
                    ImageUrl = null
                });

                context.Ratings.Add(new Rating
                {
                    Id = 1,
                    UserId = new Guid("76251b87-c54d-47a1-bca2-df905d96c557"),
                    Comment = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae ",
                    CreatedDate = Convert.ToDateTime("2022-03-12 18:38:26.0000000"),
                    ProductId = 1,
                    Star = 5,
                });

                context.SaveChanges();

            }
            var mockContext = new ApplicationDbContext(options);
            _ratingService = new RatingService(mockContext);
        }

        [Fact]
        public async Task GetRatingByProduct_ProductIdNotExist_ReturnEmptyItem()
        {
            //Arrange
            var req = new PagingRequest()
            {
                PageIndex = 1,
                PageSize = 8
            };

            int productId = 99;

            //Act
            var result = await _ratingService.GetRatingByProduct(req,productId);

            //Assert
            Assert.Empty(result.Items);
        }

        [Fact]
        public async Task GetRatingByProduct_IdExists_ReturnPagedResultModel()
        {
            //Arrange
            var req = new PagingRequest()
            {
                PageIndex = 1,
                PageSize = 8
            };

            int productId = 1;

            //Act
            var result = await _ratingService.GetRatingByProduct(req, productId);

            //Assert
            Assert.IsType<PagedResult<RatingVM>>(result);
            Assert.Equal(productId, result.Items[0].Id);
        }

        [Fact]
        public async Task GetRatingByProduct_PageIndexAndPageSizeAreEqual0_ReturnEmptyItem()
        {
            //Arrange
            var req = new PagingRequest()
            {
                PageIndex = 0,
                PageSize = 0
            };
            int productId = 1;

            //Act
            var result = await _ratingService.GetRatingByProduct(req, productId);

            //Assert
            Assert.Empty(result.Items);
        }

        [Fact]
        public async Task AddRating_StarGreaterThan5_Return0()
        {
            //Arrange
            var req = new RatingCreateRequest()
            {
                UserId = new Guid("76251b87-c54d-47a1-bca2-df905d96c557"),
                Star = 7,
                Comment = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae",
                CreatedDate = DateTime.Today,
                ProductId = 1
            };

            //Act
            var result = await _ratingService.AddRating(req);

            //Assert
            Assert.Equal(0,result);
        }

        [Fact]
        public async Task AddRating_StarLessThan0_Return0()
        {
            //Arrange
            var req = new RatingCreateRequest()
            {
                UserId = new Guid("76251b87-c54d-47a1-bca2-df905d96c557"),
                Star = -5,
                Comment = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae",
                CreatedDate = DateTime.Today,
                ProductId = 1
            };

            //Act
            var result = await _ratingService.AddRating(req);
            //Assert
            Assert.Equal(0, result);
        }
    }
}

