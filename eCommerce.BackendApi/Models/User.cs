using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.BackendApi.Models;
using eCommerce.BackendApi.Models.Enums.User;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.BackendApi.Data.Models
{
	public class User : IdentityRole<Guid>
	{
		[PersonalData]
		public string FirstName { get; set; }
		[PersonalData]
		public string LastName { get; set; }
		[PersonalData]
		public DateTime? Dob { get; set; }
		[PersonalData]
		public UserGender Gender { get; set; }
		[PersonalData]
		public string? ImageUrl { get; set; }

		//Foreign
		public List<Rating> Ratings { get; set; }

		public List<Order> Orders { get; set; }
	}
}

