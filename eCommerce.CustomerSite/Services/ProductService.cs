using System;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.Shared.Constants;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Products;
using Newtonsoft.Json;

namespace eCommerce.CustomerSite.Services
{
    public class ProductService : BaseService, IProductService
    {

        public ProductService(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<PagedResult<ProductVM>> GetTop8Product()
		{
            var client = this.CreateClient();
            var url = $"{EndpointConstants.PRODUCT_PAGING}?pageIndex=2&pageSize=8";
            return await this.GetAsync<PagedResult<ProductVM>>(url, client);

        }
        public async Task<ProductVM> GetProductById(int id)
        {
            var client = this.CreateClient();
            var url = $"{EndpointConstants.PRODUCT}{id}";
            return await this.GetAsync<ProductVM>(url, client);
        }

        public async Task<PagedResult<ProductVM>> GetProductByCategory(PagingRequest req, int categoryId)
        {
            var client = this.CreateClient();
            var url = $"{EndpointConstants.PRODUCT_CATEGORY}/{categoryId}?pageIndex={req.PageIndex}&pageSize={req.PageSize}";
            return await this.GetAsync<PagedResult<ProductVM>>(url, client);
        }
    }
}

