using System;
using Newtonsoft.Json;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.Shared.ViewModels.Users;
using System.Text;

namespace eCommerce.CustomerSite.Services
{
	public class UserService : IUserService
	{
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
            
        public UserService(IHttpClientFactory httpClientFactory,IConfiguration configuration)
		{
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
		}

        public async Task<string> Login(LoginRequest req)
        {
            var json = JsonConvert.SerializeObject(req);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("api/user/login",httpContent);
            var token = await response.Content.ReadAsStringAsync();

            return token;

        }
    }
}

