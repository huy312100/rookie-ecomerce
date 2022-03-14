using System;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Shared.ViewModels.Ratings
{
	public class RatingCreateRequest
	{
		[Required]
		public int Star { get; set; }
		public string Comment { get; set; }
		public DateTime CreatedDate { get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		public Guid UserId { get; set; }
	}
}

