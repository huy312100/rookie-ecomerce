using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Shared.ViewModels.Users
{
	public class UserUpdateRequest
	{
		[Required]
		public Guid Id { get; set; }
		[MaxLength(128)]
		public string? FirstName { get; set; }
		[MaxLength(128)]
		public string? LastName { get; set; } 
		public DateTime? Dob { get; set; }
		public IFormFile ImageUrl { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
	}
}

