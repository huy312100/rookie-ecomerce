using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.CustomerSite.ViewModels;
using eCommerce.Shared.Constants;
using eCommerce.Shared.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }


        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetProductById(id);

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Category(int id, int page = 1)
        {
            var paging = new PagingRequest()
            {
                PageIndex = page,
                PageSize = PageConstants.PAGE_PRODUCT_BY_CATEGORY,
            };

            var products = await _productService.GetProductByCategory(paging, id);
            if(products.TotalRecords == 0)
            {
                return RedirectToAction("pagenotfound", "error");
            }

            var model = new ProductCategoryVM
            {
                ProductByCategory = products
            };
            return View(model);
        }

    }
}

