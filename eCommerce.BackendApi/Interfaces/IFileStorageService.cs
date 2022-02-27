namespace eCommerce.BackendApi.Interfaces
{
	public interface IFileStorageService
	{
		Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
		Task DeleteFileAsync(string fileName);
		Task<string> SaveFile(IFormFile file);
	}
}

