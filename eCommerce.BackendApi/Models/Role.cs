using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.BackendApi.Models
{
	public class Role :IdentityRole<Guid>
	{
		[PersonalData]
		[MaxLength(256)]
		public string? Description { get; set; }
	}
}

