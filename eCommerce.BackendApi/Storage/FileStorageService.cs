using System;
using Microsoft.AspNetCore.Hosting; 
using eCommerce.BackendApi.Interfaces;
using System.Net.Http.Headers;

namespace eCommerce.BackendApi.Storage
{
	public class FileStorageService : IFileStorageService
	{
		private readonly string _fileSourceFolder;
		private const string FILE_SOURCE_FOLDER_NAME = "file-source";

		public FileStorageService(IWebHostEnvironment webHostEnvironment)
		{
            _fileSourceFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Repo/"+FILE_SOURCE_FOLDER_NAME);
		}

        public async Task<string> SaveFile(IFormFile file)
        {
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                                    .FileName.Trim('"');
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await SaveFileAsync(file.OpenReadStream(), fileName);
            return FILE_SOURCE_FOLDER_NAME + "/" + fileName;
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

