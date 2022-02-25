using System.ComponentModel.DataAnnotations;

namespace eCommerce.Shared.ViewModels.Categories
{
	public class CategoryDeleteRequest
	{
		[Required(ErrorMessage = "Please enter category id")]
		public int Id { get; set; }
	}
}

