using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.CustomerSite.ViewModels;
using eCommerce.Shared.Constants;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Ratings;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IRatingService _ratingService;

        public ProductController(ILogger<ProductController> logger, IProductService productService, IRatingService ratingService)
        {
            _logger = logger;
            _productService = productService;
            _ratingService = ratingService;
        }

        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Detail(int id, int ratingPage = 1)
        {
            var product = await _productService.GetProductById(id);

            var paging = new PagingRequest()
            {
                PageIndex = ratingPage,
                PageSize = PageConstants.PAGE_RATING_BY_PRODUCT,
            };
            var ratings = await _ratingService.GetRatingByProduct(paging, id);

            var model = new ProductDetailVM
            {
                Product = product,
                ProductRatings = ratings
            };

            return View(model);
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
            if (products.TotalRecords == 0)
            {
                return RedirectToAction("pagenotfound", "error");
            }

            var model = new ProductCategoryVM
            {
                ProductByCategory = products
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SubmitReview(int id, int ratingPage = 1)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Detail(IFormCollection form)
        {
            var id = User.FindFirst("UserId")?.Value;
            

            //var allNeededKeys = formCollection.AllKeys.Where(x => x.StartsWith("hidden_tag"));
            //form.a

            var ratingCreateRequest = new RatingCreateRequest()
            {
                UserId = Guid.Parse(id),
                ProductId=Int32.Parse(form["ProductIdSubmit"].ToString()),
                Comment = form["Review"],
                CreatedDate=DateTime.Now,
                Star = Int32.Parse(Request.Form["StarSubmit"].ToString())
            };
            var res = await _ratingService.CreateRating(ratingCreateRequest);
            if (!res)
            {
                return RedirectToAction("pagenotfound", "error");
            }

            return Redirect($"/product/detail/{ratingCreateRequest.ProductId}");
        }
    }
}

