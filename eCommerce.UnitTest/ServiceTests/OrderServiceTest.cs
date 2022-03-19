using System;
using System.Threading.Tasks;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Models;
using eCommerce.BackendApi.Services;
using eCommerce.Shared.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace eCommerce.UnitTest.ServiceTests
{
    public class OrderServiceTest
	{
        private readonly OrderService _orderService;

        public OrderServiceTest()
		{
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Orders.Add(new Order
                {
					Id = 1,
					OrderDate = DateTime.Today, 
					Address = "110 Nguyen Dinh Chinh",
					Email = "huy312100@gmail.com",
					PhoneNumber = "0329755057",
					Note = "Lorem ipsum dolor sit amet consectetur adipisicing elit",
					Status = (Shared.Enums.Order.OrderStatus)1,
					PaymentType = (Shared.Enums.Order.OrderPayment)1,
					Total = 50000,
					UserId = new Guid("76251b87-c54d-47a1-bca2-df905d96c557")
                });

				context.OrderDetails.Add(new OrderDetail
				{
					Id = 1,
					Quantity = 2,
					OrderId = 1,
					ProductId = 1,
					SubTotal = 1000000
				});

				context.OrderDetails.Add(new OrderDetail
				{
					Id = 2,
					Quantity = 1,
					OrderId = 1,
					ProductId = 3,
					SubTotal = 150000
				});

				context.SaveChanges();

            }
            var mockContext = new ApplicationDbContext(options);
            _orderService = new OrderService(mockContext);
        }

		[Fact]
		public async Task GetOrdersPaging_PageIndexAndPageSizeAreEqual0_ReturnEmptyItem()
		{
			//Arrange
			var req = new PagingRequest()
			{
				PageIndex = 0,
				PageSize = 0
			};

			Guid userId = new Guid("76251b87-c54d-47a1-bca2-df905d96c557");
			//Act
			var result = await _orderService.GetOrdersPaging(req,userId);

			//Assert
			Assert.Empty(result.Items);
		}
	}
}

