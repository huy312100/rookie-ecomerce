using System;
using System.IO;
using System.Threading.Tasks;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Models;
using eCommerce.BackendApi.Services;
using eCommerce.Shared.ViewModels.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eCommerce.UnitTest.ServiceTests
{
	public class CategoryServiceTest
	{
		private readonly CategoryService _categoryService;

        public CategoryServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "Smartphone & Tablet",
                    Description = "Lorem ipsum dolor sit amet",
                    CreatedDate=DateTime.Today,
                    ImageUrl = "/file-source/abc.jpg",
                    ParentId=null
                });

                context.SaveChanges();

            }
            var mockContext = new ApplicationDbContext(options);
            var fileService = FileStorageService.IStorageService();
            _categoryService = new CategoryService(mockContext, fileService);
        }

        private IFormFile MockImage()
        {
            var file = new Mock<IFormFile>();
            var content = "source image path for unitTest";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            var fileName = "test.png";
            file.Setup(f => f.OpenReadStream()).Returns(ms);
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(_ => _.ContentDisposition)
                .Returns($"form-data;name='file';filename ='{fileName}'");
            file.Setup(f => f.Length).Returns(ms.Length);
            return file.Object;
        }

        [Fact]
        public async Task GetCategoryById_IdExists_ReturnACategoryVM()
        {
            //Arrange
            int categoryId = 1;
            // Act 
            var result = await _categoryService.GetCategoryById(categoryId);
            //// Assert
            Assert.IsType<CategoryVM>(result);
            Assert.Equal(categoryId, result.Id);
        }

        [Fact]
        public async Task GetCategoryById_IdNotFound_ThrowException()
        {
            //Arrange
            int categoryId = 123;
            // Act 
            var res = _categoryService.GetCategoryById(categoryId);
            // Assert
            Assert.Contains($"Cannot find category with ID {categoryId}", res.Exception.Message);
        }

        [Fact]
        public async Task CreateCategory_ParameterValid_ThrowException()
        {
            //Arrange
            var image = MockImage();
            var req = new CategoryCreateRequest()
            {
                Name = "New Category",
                Description = "Lorem ipsum dolor sit amet",
                Image=image,
                ParentId=null
            };
            // Act 
            var newCategoryId = await _categoryService.CreateCategory(req);
            var newCategory = await _categoryService.GetCategoryById(newCategoryId);

            // Assert
            Assert.IsType<int>(newCategoryId);
            Assert.IsType<CategoryVM>(newCategory);
            Assert.Equal(newCategoryId, newCategory.Id);
        }

        [Fact]
        public async Task UpdateCategory_CheckCategoryUpdatedSuccess_ReturnTrue()
        {
            //Arrange
            var image = MockImage();
            var req = new CategoryUpdateRequest()
            {
                Id = 1,
                Description = "Lorem ipsum dolor sit amet",
                Image = image,
                Name = "New Product",
                ParentId=null
            };
            // Act 
            var result = await _categoryService.UpdateCategory(req);
            var categoryAfterUpdate = await _categoryService.GetCategoryById(req.Id);

            // Assert
            Assert.IsType<int>(result);
            Assert.IsType<CategoryVM>(categoryAfterUpdate);
            Assert.Equal(categoryAfterUpdate.Id, req.Id);
            Assert.Equal(categoryAfterUpdate.Name, req.Name);
            Assert.Equal(categoryAfterUpdate.Description, req.Description);
        }

        [Fact]
        public async Task DeleteCategory_CategoryIsNotExist_ThrowException()
        {
            // Arrange
            int categoryId = 99;
            // Act
            Func<Task> act = async () => await _categoryService.DeleteCategory(categoryId);
            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains($"Cannot delete category because CategoryID {categoryId} is null not found", ex.Message);
        }

        [Fact]
        public async Task DeleteCategory_CheckCategoryIdNotExistAfterDelete_ReturnTrue()
        {
            // Arrange
            int categoryId = 1;
            // Act
            var result = await _categoryService.DeleteCategory(categoryId);
            Func<Task> act = async () => await _categoryService.GetCategoryById(categoryId);
            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.True(result > 0);
            Assert.Contains($"Cannot find category with ID {categoryId}", ex.Message);
        }
    }
}

