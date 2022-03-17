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
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBrand()
        {
            var res = await _brandService.GetAllBrands();
            if (res == null)
            {
                BadRequest();
            }
            return Ok(res);
        }
    }
}

