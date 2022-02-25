using System;
using eCommerce.BackendApi.Models;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.BackendApi.Services
{
	public class UserService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public UserService(UserManager<User> userManager,SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}


	}
}

