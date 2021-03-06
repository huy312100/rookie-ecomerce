using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eCommerce.BackendApi.Interfaces;
using eCommerce.Shared.Constants;
using eCommerce.Shared.ViewModels.Common;
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
        public async Task<IActionResult> GetOrdersPaging(PagingRequest req,Guid userId)
        {
            var res = await _orderService.GetOrdersPaging(req,userId);
            if (res == null)
            {
                return BadRequest(ErrorConstants.APIGetOrderError);
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CheckoutOrder([FromBody] CheckoutRequest req)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(ErrorConstants.APIPermissionDenied);
            }
            var userId = User.FindFirst("UserId")?.Value;

            if (userId != req.UserId.ToString())
            {
                return BadRequest(ErrorConstants.APIPermissionDenied);
            }
            var res = await _orderService.CheckoutOrder(req);
            if (res < 0)
            {
                return BadRequest(ErrorConstants.APIOrderError);
            }
            return Ok(res);
        }
    }
}

