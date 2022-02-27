using System;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Shared.ViewModels.Users
{
	public class LoginRequest
	{
		[Required]
		public string Username { get; set; } = null!;
		[Required]
		public string Password { get; set; } = null!;
		public bool RememberMe { get; set; }
	}
}

