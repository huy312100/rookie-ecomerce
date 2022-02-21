using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.BackendApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.BackendApi.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _prodService;

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var prod = await _prodService.GetAllProducts();
            return Ok(prod);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}

