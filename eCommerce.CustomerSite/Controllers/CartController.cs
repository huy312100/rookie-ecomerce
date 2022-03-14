using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.CustomerSite.ViewModels;
using eCommerce.Shared.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.CustomerSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly string _cartSession = "CartSession";
        public CartController(ILogger<CartController> logger,IProductService productService,IOrderService orderService)
        {
            _logger = logger;
            _productService = productService;
            _orderService = orderService;
        }

        // GET: /<controller>/
        public IActionResult Detail()
        {
            List<CartItemVM> currentCart = new List<CartItemVM>();
            currentCart = GetCart();
            var cartVM = new CartVM()
            {
                Items = currentCart,
                Total = Math.Round(currentCart.Sum(x => x.Price * x.Quantity), 2)
            };
            return View(cartVM);
        }

        public IActionResult Checkout()
        {
            List<CartItemVM> currentCart = new List<CartItemVM>();
            currentCart = GetCart();
            var cartVM = new CartVM()
            {
                Items = currentCart,
                Total = Math.Round(currentCart.Sum(x => x.Price * x.Quantity), 2)
            };
            var checkoutVM= new CheckoutVM()
            {
                Cart = cartVM,
                CheckoutModel = new CheckoutRequest()
            };

            return View(checkoutVM);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(IFormCollection form)
        {
            var id = User.FindFirst("UserId")?.Value;
            if (id == null)
            {
                TempData["ErrorMessage"] = "You must login to checkout";
                return RedirectToAction("Index", "Cart");
            }
            List<CartItemVM> currentCart = new List<CartItemVM>();
            currentCart = GetCart();

            
            var orderDetails = new List<OrderDetailCreateRequest>();
            foreach (var item in currentCart)
            {
                orderDetails.Add(new OrderDetailCreateRequest()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity               
                });
            }

            var checkoutRequest = new CheckoutRequest()
            {
                UserId = Guid.Parse(id),
                Address = form["Address"],
                //FullName = model.CheckoutModel.FullName,
                Email = form["Email"],
                PhoneNumber = form["PhoneNumber"],
                OrderDetails = orderDetails,
                Note = form["Note"],
                PaymentType = (Shared.Enums.Order.OrderPayment)ConvertPaymentTypeToInt(form["payment_option"])
            };
            var res=await _orderService.Checkout(checkoutRequest);
            if (res)
            {
                HttpContext.Session.Remove(_cartSession);
                return RedirectToAction("index", "home");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _productService.GetProductById(id);

            List<CartItemVM> currentCart = new List<CartItemVM>();
            currentCart = GetCart();
            if (currentCart.Any(x => x.ProductId == id))
            {
                currentCart.First(x => x.ProductId == id).Quantity += 1;
            }
            else
            {
                var cartItem = new CartItemVM()
                {
                    ProductId = id,
                    //Image = product.Images.,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                };

                currentCart.Add(cartItem);
            }

            HttpContext.Session.SetString(_cartSession, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }

        public IActionResult RemoveItem(int id)
        {
            List<CartItemVM> currentCart = new List<CartItemVM>();
            currentCart = GetCart();
            var item = currentCart.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                currentCart.Remove(item);
            }
            HttpContext.Session.SetString(_cartSession, JsonConvert.SerializeObject(currentCart));
            return RedirectToAction("Detail", "Cart");
        }

        private List<CartItemVM> GetCart()
        {
            var session = HttpContext.Session.GetString(_cartSession);
            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            return currentCart;
        }

        private int ConvertPaymentTypeToInt(string data)
        {
            if(data == "option3")
            {
                return 0;
            }
            else if(data == "option4")
            {
                return 1;
            }
            else { return 2; }
        }
    }
}

