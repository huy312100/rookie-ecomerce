using System;
namespace eCommerce.Shared.ViewModels.Ratings
{
	public class RatingVM
	{
		public int Id { get; set; }
		public int Star { get; set; }
		public string Comment { get; set; }
		public DateTime CreatedDate { get; set; }
		public int ProductId { get; set; }
		public Guid UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string ImageUrl { get; set; }
	}
}

