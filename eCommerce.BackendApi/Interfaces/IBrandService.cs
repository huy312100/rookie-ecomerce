using System;
using eCommerce.Shared.ViewModels.Brands;

namespace eCommerce.BackendApi.Interfaces
{
	public interface IBrandService
	{
		Task<List<BrandVM>> GetAllBrands();
	}
}

