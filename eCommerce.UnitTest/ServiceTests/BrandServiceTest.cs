using System;
using System.Threading.Tasks;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Models;
using eCommerce.BackendApi.Services;
using eCommerce.Shared.ViewModels.Brands;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace eCommerce.UnitTest.ServiceTests
{
	public class BrandServiceTest
	{
        private readonly BrandServices _brandService;

        public BrandServiceTest()
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

                context.Brands.Add(new Brand
                {
                    Id = 2,
                    Name = "Samsung",
                    Description = "Lorem ipsum dolor sit amet",
                });

                context.Brands.Add(new Brand
                {
                    Id = 3,
                    Name = "Xiaomi",
                    Description = "Lorem ipsum dolor sit amet",
                });

                context.SaveChanges();

            }
            var mockContext = new ApplicationDbContext(options);
            _brandService = new BrandServices(mockContext);
        }

        //[Fact]
        //public async Task GetAllBrand_ReturnListBrandVM()
        //{
        //    //Arrange
        //    // Act 
        //    var result = await _brandService.GetAllBrands();
        //    //// Assert
        //    Assert.IsType<List<BrandVM>(result);
        //}
    }
}

