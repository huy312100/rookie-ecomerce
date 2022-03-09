using System;
using Newtonsoft.Json;

namespace eCommerce.CustomerSite.Services
{
	public class BaseService
	{
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        protected BaseService(IHttpClientFactory httpClientFactory,
                    IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }


        protected HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            return client;
        }

        protected async Task<TResponse> GetAsync<TResponse>(string url, HttpClient client)
        {
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(body,
                    typeof(TResponse));
                return myDeserializedObjList;
            }
            return JsonConvert.DeserializeObject<TResponse>(body);
        }

        protected async Task<List<T>> GetListAsync<T>(string url, HttpClient client)
        {
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var data = (List<T>)JsonConvert.DeserializeObject(body, typeof(List<T>));
                return data;
            }
            throw new Exception(body);
        }
    }
}

