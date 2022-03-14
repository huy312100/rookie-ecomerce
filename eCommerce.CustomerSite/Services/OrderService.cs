using System;
using System.Text;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.Shared.Constants;
using eCommerce.Shared.ViewModels.Orders;
using Newtonsoft.Json;

namespace eCommerce.CustomerSite.Services
{
	public class OrderService :BaseService,IOrderService
	{
        public OrderService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        public async Task<bool> Checkout(CheckoutRequest request)
        {
            var client = this.CreateClient();

            var url = $"{EndpointConstants.ORDER}";

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, httpContent);
            var msg=response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }//
    }
}

