using System;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using eCommerce.Shared.ViewModels.Categories;
using eCommerce.Shared.ViewModels.Common;
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

		public async Task<PagedResult<OrderVM>> GetOrdersPaging(PagingRequest req,Guid userId)
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

			int totalRow = await query.CountAsync();

			var data = await query.Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize)
			.Select(res => new OrderVM()
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

			var pagedResult = new PagedResult<OrderVM>()
			{
				TotalRecords = totalRow,
				PageSize = req.PageSize,
				PageIndex = req.PageIndex,
				Items = data
			};
			return pagedResult;
        }

		public async Task<int> CheckoutOrder(CheckoutRequest req)
        {
			double totalPrice = 0;
            var orderDetails = new List<OrderDetail>();
			foreach (var item in req.OrderDetails)
			{
				var query = await _dbContext.Products
								.Where(prop => prop.Id == item.ProductId)
								.Select(data => new { data.Price }).FirstOrDefaultAsync();

				//var subTotal = (from p in _dbContext.Products
				//                where p.Id == item.ProductId
				//                select new { p.Price }).ToString();

				var subTotal = query.Price * item.Quantity;

				orderDetails.Add(new OrderDetail()
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity,
					SubTotal = subTotal
				}) ;
				totalPrice += subTotal;
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
			//await _dbContext.SaveChangesAsync();
			//int orderId = order.Id;

			//foreach (var detail in orderDetails.ToList())
   //         {
			//	var itemOrderDetails = new OrderDetail()
			//	{
			//		ProductId = detail.ProductId,
			//		Quantity = detail.Quantity,
			//		SubTotal = detail.SubTotal,
			//		OrderId = orderId
			//	};
			//	_dbContext.OrderDetails.Add(itemOrderDetails);
			//}
			return await _dbContext.SaveChangesAsync();
		}
	}
}

