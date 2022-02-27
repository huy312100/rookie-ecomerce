using System;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Shared.ViewModels.Users
{
	public class UserDeleteRequest
	{
		[Required]
		public Guid Id { get; set; }
	}
}

