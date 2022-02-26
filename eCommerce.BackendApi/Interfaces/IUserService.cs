using eCommerce.Shared.ViewModels.Users;

namespace eCommerce.BackendApi.Interfaces
{
	public interface IUserService
	{
		Task<bool> Register(RegisterRequest req);
		Task<string?> Login(LoginRequest req);
	}
}

