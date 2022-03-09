using System;
using System.Security.Claims;
using eCommerce.Shared.ViewModels.Users;

namespace eCommerce.CustomerSite.Interfaces
{
	public interface IUserService
	{
		Task<string> Login(LoginRequest req);
		ClaimsPrincipal ValidateToken(string jwtToken);
		Task<bool> RegisterUser(RegisterRequest registerRequest);
	}
}

