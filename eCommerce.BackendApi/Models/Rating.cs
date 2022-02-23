using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.BackendApi.Models
{
	public class Rating
	{
		[Key]
		public int Id { get; set; }
		[Range(0,5)]
		public int? Star { get; set; }
		[StringLength(300)]
		public string? Comment { get; set; }
		public DateTime? CreatedDate { get; set; }
		public int ProductId { get; set; }
		public Guid UserId { get; set; }

		//Forign key
		public virtual Product Product { get; set; }

		public virtual User User { get; set; }

	}
}

