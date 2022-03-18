using System;
using System.Text;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.Shared.Constants;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Ratings;
using Newtonsoft.Json;

namespace eCommerce.CustomerSite.Services
{
	public class RatingService : BaseService, IRatingService
	{
		public RatingService(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
			: base(httpClientFactory, httpContextAccessor, configuration)
		{
		}

        public async Task<bool> CreateRating(RatingCreateRequest req)
        {
            var client = this.CreateClientWithBearerToken();

            var url = $"{EndpointConstants.RATING}";

            var json = JsonConvert.SerializeObject(req);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, httpContent);
            var msg = response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<PagedResult<RatingVM>> GetRatingByProduct(PagingRequest req, int productId)
        {
            var client = this.CreateClient();
            var url = $"{EndpointConstants.PRODUCT_RATING}/{productId}?pageIndex={req.PageIndex}&pageSize={req.PageSize}";
            return await this.GetAsync<PagedResult<RatingVM>>(url, client);
        }

    }
}

