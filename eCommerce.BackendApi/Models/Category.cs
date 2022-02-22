using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.BackendApi.Models
{
    [Table("Categories")]
	public class Category
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ParentId { get; set; }

        //Foreign
        public virtual Category? Parent { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}

