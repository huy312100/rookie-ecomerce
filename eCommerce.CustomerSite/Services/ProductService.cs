﻿using System;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.Shared.Constants;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Products;
using Newtonsoft.Json;

namespace eCommerce.CustomerSite.Services
{
    public class ProductService : BaseService, IProductService
    {

        public ProductService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        public async Task<PagedResult<ProductVM>> GetTop8Product()
		{
            var client = this.CreateClient();
            var url = $"{EndpointConstants.PRODUCT_PAGING}?pageIndex=1&pageSize=8";
            return await this.GetAsync<PagedResult<ProductVM>>(url, client);

        }

    }
}

