using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.BackendApi.Interfaces;
using eCommerce.Shared.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.BackendApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategory()
        {
            var res = await _categoryService.GetAllCategories();
            if(res == null)
            {
                BadRequest();
            }
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var res = await _categoryService.GetCategoryById(id);

            if(res == null)
            {
                BadRequest();
            }

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryId = await _categoryService.CreateCategory(req);
            if (categoryId < 0)
                return BadRequest();
            var category = await _categoryService.GetCategoryById(categoryId);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryId }, category);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var res = await _categoryService.DeleteCategory(categoryId);
            if (res < 0)
            {
                return BadRequest();
            }

            return Ok(res);
        }
    }
}

