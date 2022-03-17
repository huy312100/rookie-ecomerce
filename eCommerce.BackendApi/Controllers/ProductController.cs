using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.BackendApi.Interfaces;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.BackendApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _prodService;

        public ProductController(IProductService prodService)
        {
            _prodService = prodService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var res = await _prodService.GetAllProducts();
            return Ok(res);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllProductPaging([FromQuery] PagingRequest req)
        {
            var products = await _prodService.GetProductPaging(req);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var res = await _prodService.GetProductById(id);
            if(res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            var res = await _prodService.GetImageById(id);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductByCategory([FromQuery] PagingRequest req,int categoryId)
        {
            var res = await _prodService.GetProductByCategory(req,categoryId);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest req)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var productId = await _prodService.CreateProduct(req);
            if (productId < 0)
                return BadRequest();
            var product = await _prodService.GetProductById(productId);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _prodService.UpdateProduct(req);
            if (res < 0)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var res = await _prodService.DeleteProduct(productId);
            if (res < 0)
            {
                return BadRequest();
            }

            return Ok(res);
        }

    }
}

