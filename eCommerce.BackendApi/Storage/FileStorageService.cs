using System;
using Microsoft.AspNetCore.Hosting; 
using eCommerce.BackendApi.Interfaces;

namespace eCommerce.BackendApi.Storage
{
	public class FileStorageService : IFileStorageService
	{
		private readonly string _fileSourceFolder;
		private const string FILE_SOURCE_FOLDER_NAME = "Repo/file-source";

		public FileStorageService(IWebHostEnvironment webHostEnvironment)
		{
            _fileSourceFolder = Path.Combine(webHostEnvironment.ContentRootPath, FILE_SOURCE_FOLDER_NAME);
		}

        public string GetFileUrl(string fileName)
        {
            return $"Repo/{FILE_SOURCE_FOLDER_NAME}/{fileName}";
    
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_fileSourceFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_fileSourceFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}

