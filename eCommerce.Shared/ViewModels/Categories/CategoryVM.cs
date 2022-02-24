using System;
namespace eCommerce.Shared.ViewModels.Categories
{
	public class CategoryVM
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ParentId { get; set; }

    }
}

