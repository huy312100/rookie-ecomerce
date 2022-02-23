using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.BackendApi.Interfaces;
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
        public async Task<IActionResult> GetProductByCategory(int categoryId)
        {
            var res = await _prodService.GetProductByCategory(categoryId);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }


    }
}

