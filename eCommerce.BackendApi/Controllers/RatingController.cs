using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.BackendApi.Interfaces;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Ratings;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.BackendApi.Controllers
{
    [Route("api/[controller]")]
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating([FromBody] RatingCreateRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _ratingService.AddRating(req);
            if (res < 0)
            {
                return BadRequest();
            }
            
            return Ok(res);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetRatingByProduct([FromQuery] PagingRequest req, int productId)
        {
            var res = _ratingService.GetRatingByProduct(req, productId);
            return Ok(res);
        }

        //[HttpGet("{productId}")]
        //public async Task<IActionResult> GetRatingByProduct(int productId)
        //{
        //    var res = _ratingService.GetRatingByProduct(productId);
        //    return Ok(res);
        //}
    }
}

