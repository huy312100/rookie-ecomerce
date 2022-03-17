using System;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Interfaces;
using eCommerce.Shared.ViewModels.Brands;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.BackendApi.Services
{
	public class BrandServices : IBrandService
	{
        private readonly ApplicationDbContext _dbContext;

        public BrandServices(ApplicationDbContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task<List<BrandVM>> GetAllBrands()
        {
            var query = from b in _dbContext.Brands
                        select b;

            var data = await _dbContext.Brands.Select(res => new BrandVM
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
            }).ToListAsync();
            return data;
        }
    }
}

