using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.BackendApi.Models.Enums.User;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.BackendApi.Models
{
	public class User : IdentityUser<Guid>
	{
		[PersonalData]
		[Required]
		[MaxLength(128)]
		public string FirstName { get; set; }
		[PersonalData]
		[Required]
		[MaxLength(128)]
		public string LastName { get; set; }
		[PersonalData]
		public DateTime? Dob { get; set; }
		[PersonalData]
		public UserGender Gender { get; set; }
		[PersonalData]
		public string? ImageUrl { get; set; }

		//Foreign
		public virtual List<Rating> Ratings { get; set; }
		public virtual List<Order> Orders { get; set; }
	}
}

