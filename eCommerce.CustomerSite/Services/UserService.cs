using System;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.Shared.ViewModels.Users;
using System.Text;
using System.Security.Claims;
using eCommerce.Shared.Constants;

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
            var response = await client.PostAsync($"{EndpointConstants.USER_LOGIN}",httpContent);
            var token = await response.Content.ReadAsStringAsync();

            return token;

        }

        public async Task<bool> RegisterUser(RegisterRequest registerRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(registerRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{EndpointConstants.USER_REGISTER}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }


        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["JwtAuthentication:Issuer"];
            validationParameters.ValidIssuer = _configuration["JwtAuthentication:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuthentication:Key"]));


            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            var identity = new ClaimsIdentity(principal.Identity);
            identity.AddClaim(new Claim("Token", jwtToken));

            return new ClaimsPrincipal(identity);
        }
    }
}

