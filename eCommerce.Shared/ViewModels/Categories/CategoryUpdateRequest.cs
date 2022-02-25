using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Shared.ViewModels.Categories
{
	public class CategoryUpdateRequest
	{
        [Required(ErrorMessage = "Please enter category id")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public int? ParentId { get; set; }
    }
}

