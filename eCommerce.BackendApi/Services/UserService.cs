using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using eCommerce.Shared.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace eCommerce.BackendApi.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;

		public UserService(UserManager<User> userManager,SignInManager<User> signInManager,
            RoleManager<Role> roleManager,IConfiguration config)
		{
			_userManager = userManager;
			_signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
		}

        public async Task<string?> Login(LoginRequest req)
        {
            var user = await _userManager.FindByNameAsync(req.Username);
            if(user == null)
            {
                return null;
            }

            var res = await _signInManager.PasswordSignInAsync(user, req.Password, req.RememberMe, true);
            if (!res.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.GivenName,user.LastName),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,String.Join(";",roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtAuthentication:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["JwtAuthentication:Issuer"],
                _config["JwtAuthentication:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(RegisterRequest req)
        {
            var user= await _userManager.FindByNameAsync(req.Username);

            if(user != null)
            {
                throw new Exception("Username already exists");
            }

            if(await _userManager.FindByEmailAsync(req.Email) != null)
            {
                throw new Exception("Email already exists");
            }

            user = new User()
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Dob = req.Dob,
                Email = req.Email,
                PhoneNumber = req.PhoneNumber,
                UserName = req.Username,
            };

            var res = await _userManager.CreateAsync(user, req.Password);
            if (res.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}

