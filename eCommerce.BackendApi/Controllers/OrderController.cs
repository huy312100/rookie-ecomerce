using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.BackendApi.Interfaces;
using eCommerce.Shared.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.BackendApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllOrders(Guid userId)
        {
            var res = await _orderService.GetAllOrders(userId);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutOrder([FromForm] CheckoutRequest req)
        {
            var res = await _orderService.CheckoutOrder(req);
            if (res < 0)
            {
                return BadRequest();
            }
            return Ok(res);
        }
    }
}

