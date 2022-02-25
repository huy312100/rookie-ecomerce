using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Shared.ViewModels.Categories
{
	public class CategoryCreateRequest
	{
        [Required(ErrorMessage = "Please enter category name")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public int? ParentId { get; set; }

    }
}

