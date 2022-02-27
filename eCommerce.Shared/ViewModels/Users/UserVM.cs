using System;
namespace eCommerce.Shared.ViewModels.Users
{
	public class UserVM
	{
		public Guid Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		//public int Gender { get; set; }
		public DateTime? Dob { get; set; }
		public string? ImageUrl { get; set; }
		public string? PhoneNumber { get; set; }
		public string Username { get; set; }
		public string? Email { get; set; }
	}
}

