using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.BackendApi.Controllers
{
    public class RatingController : Controller
    {
        [HttpPost("rating")]
        public async Task<IActionResult> CreateRating([FromBody] ProductRatingRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = User.FindFirst("UserId")?.Value;
            request.UserId = new Guid(id);
            var rating = await _productService.AddRating(request);
            if (!rating)
                return BadRequest();
            return Ok();
        }
    }
}

