using System;
using System.Net.Http.Headers;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using eCommerce.Shared.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.BackendApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileStorageService _fileStorageService;
        private const string FILE_SOURCE_FOLDER_NAME = "file-source";

        public CategoryService(ApplicationDbContext dbContext, IFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _fileStorageService = fileStorageService;
        }

        public async Task<List<CategoryVM>> GetAllCategories()
        {
            var query = from c in _dbContext.Categories
                        select c;

            var data = await _dbContext.Categories.Select(res => new CategoryVM
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                ImageUrl = res.ImageUrl,
                CreatedDate = res.CreatedDate,
                ParentId = res.ParentId
            }).ToListAsync();
            return data;
        }


        public async Task<CategoryVM> GetCategoryById(int id)
        {
            var categories = await _dbContext.Categories.FindAsync(id);

            if(categories == null)
            {
                throw new Exception($"Cannot find category with ID {id}");
            }

            var categoryViewModel = new CategoryVM()
            {
                Id = categories.Id,
                Name = categories.Name,
                Description = categories.Description,
                ImageUrl = categories.ImageUrl,
                CreatedDate = categories.CreatedDate,
                ParentId = categories.ParentId
            };

            return categoryViewModel;
        }

        public async Task<int> CreateCategory(CategoryCreateRequest req)
        {
            var category = new Category()
            {
                Name = req.Name,
                Description = req.Description,
                CreatedDate = DateTime.Now,
                ParentId = req.ParentId
            };

            if (req.Image != null)
            {
                category.ImageUrl = await SaveFile(req.Image);
            }
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> DeleteCategory(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);

            if (category == null)
            {
                throw new Exception($"Cannot delete category because CategoryID not found");
            }

            await _fileStorageService.DeleteFileAsync(category.ImageUrl);

            _dbContext.Categories.Remove(category);
            return await _dbContext.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                                    .FileName.Trim('"');
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return FILE_SOURCE_FOLDER_NAME + "/" + fileName;
        }


    }
}

