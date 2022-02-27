using eCommerce.Shared.ViewModels.Users;

namespace eCommerce.BackendApi.Interfaces
{
	public interface IUserService
	{
		Task<bool> Register(RegisterRequest req);
		Task<string?> Login(LoginRequest req);
		Task<List<UserVM>> GetAllUser();
		Task<UserVM> GetUserById(Guid id);
		Task<bool> UpdateUser(UserUpdateRequest req);
		Task<bool> DeleteUser(UserDeleteRequest req);
	}
}

