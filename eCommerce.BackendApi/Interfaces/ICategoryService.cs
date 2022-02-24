﻿using System;
using eCommerce.Shared.ViewModels.Categories;

namespace eCommerce.BackendApi.Interfaces
{
	public interface ICategoryService
	{
		Task<List<CategoryVM>> GetAllCategories();
		Task<CategoryVM> GetCategoryById(int id);
		Task<int> CreateCategory(CategoryCreateRequest req);
		Task<int> DeleteCategory(int categoryId);
	}
}
