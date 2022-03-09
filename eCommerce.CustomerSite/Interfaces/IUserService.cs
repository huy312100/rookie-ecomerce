using System;
using eCommerce.Shared.ViewModels.Users;

namespace eCommerce.CustomerSite.Interfaces
{
	public interface IUserService
	{
		Task<string> Login(LoginRequest req);
	}
}

