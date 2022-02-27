using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Shared.ViewModels.Users
{
	public class RegisterRequest
	{
		[MaxLength(128)]
		[Required]
		public string FirstName { get; set; } = null!;
		[MaxLength(128)]
		[Required]
		public string LastName { get; set; } = null!;
		public DateTime? Dob { get; set; }
		public IFormFile ImageUrl { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		[Required]
		public string Username { get; set; } = null!;
		[Required]
		public string Password { get; set; } = null!;
	}
}

