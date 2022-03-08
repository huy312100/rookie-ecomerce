using System;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using eCommerce.Shared.ViewModels.Categories;
using eCommerce.Shared.ViewModels.Orders;
using eCommerce.Shared.ViewModels.Products;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.BackendApi.Services
{
	public class OrderService : IOrderService
	{
		private readonly ApplicationDbContext _dbContext;
		public OrderService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<OrderVM>> GetAllOrders(Guid userId)
        {
			var query = from o in _dbContext.Orders
						join od in _dbContext.OrderDetails
						on o.Id equals od.OrderId
						join p in _dbContext.Products
						on od.ProductId equals p.Id
						join c in _dbContext.Categories
						on p.CategoryId equals c.Id
						join pi in _dbContext.ProductImages
						on p.Id equals pi.ProductId
						where o.UserId == userId
						select new { o, od, p, c, pi };

			var data = await query.Select(res => new OrderVM()
			{
				Id= res.o.Id,
				OrderDate = res.o.OrderDate,
				Address=res.o.Address,
				Email=res.o.Email,
				PhoneNumber=res.o.PhoneNumber,
				Note =res.o.Note,
				Status=res.o.Status,
				PaymentType=res.o.PaymentType,
				Total=res.o.Total,
				OrderDetails = query.Select(dataOrderDetail => new OrderDetailVM()
				{
					Quantity = dataOrderDetail.od.Quantity,
					Product = new ProductVM()
					{
						Id = res.p.Id,
						Name = res.p.Name,
						Price = res.p.Price,
						Category = new CategoryVM
						{
							Id = res.c.Id,
							Name = res.c.Name
						},
						Images = query.Select(dataProductImage => new ProductImageVM()
						{
							Id = dataProductImage.pi.Id,
							ImageUrl = dataProductImage.pi.ImageUrl,
							IsThumbnail = dataProductImage.pi.IsThumbnail
						}).ToList()
					}
				}).ToList()
            }).ToListAsync();
			return data;
        }

		public async Task<int> CheckoutOrder(CheckoutRequest req)
        {
			double totalPrice = 0;
            var orderDetails = new List<OrderDetail>();
			foreach (var item in req.OrderDetails)
			{
				orderDetails.Add(new OrderDetail()
				{
					ProductId = item.Product.Id,
					Quantity = item.Quantity,
					SubTotal = item.Product.Price * item.Quantity,
				});
				totalPrice += item.Product.Price * item.Quantity;
			}

			var order = new Order()
			{
				UserId = req.UserId,
				OrderDate = DateTime.Now,
				Address = req.Address,
				Email = req.Email,
				PhoneNumber = req.PhoneNumber,
				Note = req.Note,
				Status = 0,
				PaymentType = req.PaymentType,
				OrderDetails = orderDetails,
				Total = totalPrice
			};

			_dbContext.Orders.Add(order);
			await _dbContext.SaveChangesAsync();
			int orderId = order.Id;

			foreach (var detail in orderDetails)
            {
				var itemOrderDetails = new OrderDetail()
				{
					ProductId = detail.Product.Id,
					Quantity = detail.Quantity,
					SubTotal = detail.Product.Price * detail.Quantity,
					OrderId = orderId
				};
				_dbContext.OrderDetails.Add(itemOrderDetails);
			}
			return await _dbContext.SaveChangesAsync();
		}
	}
}

