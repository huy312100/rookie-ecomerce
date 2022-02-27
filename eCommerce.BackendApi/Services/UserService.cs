using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using eCommerce.Shared.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace eCommerce.BackendApi.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        private readonly IFileStorageService _fileStorageService;

        public UserService(UserManager<User> userManager,SignInManager<User> signInManager,
            RoleManager<Role> roleManager,IConfiguration config,
            IFileStorageService fileStorageService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _fileStorageService = fileStorageService;
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

            if(req.ImageUrl != null)
            {
                user.ImageUrl = await _fileStorageService.SaveFile(req.ImageUrl);
            }

            var res = await _userManager.CreateAsync(user, req.Password);
            if (res.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<List<UserVM>> GetAllUser()
        {
            var data = await _userManager.Users.Select(res => new UserVM()
            {
                Id = res.Id,
                FirstName = res.FirstName,
                LastName = res.LastName,
                //Gender = res.Gender,
                ImageUrl = res.ImageUrl,
                Dob = res.Dob,
                PhoneNumber = res.PhoneNumber,
                Username = res.UserName,
                Email = res.Email
            }).ToListAsync();
            return data;
        }

        public async Task<UserVM> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null)
            {
                throw new Exception("User is not existed");
            }

            var userViewModel = new UserVM()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob,
                ImageUrl= user.ImageUrl,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName,
                Email = user.Email
            };

            return userViewModel;
        }

        public async Task<bool> UpdateUser(UserUpdateRequest req)
        {
            if (await _userManager.Users.AnyAsync(prop => prop.Email == req.Email && prop.Id != req.Id))
            {
                throw new Exception("Email existed");
            }
            var user = await _userManager.FindByIdAsync(req.Id.ToString());

            if(user == null)
            {
                throw new Exception($"Cannot update user because CategoryID {req.Id} is null or not found");
            }

            if (req.FirstName != null)
            {
                user.FirstName = req.FirstName;
            }
            if(req.LastName != null)
            {
                user.LastName = req.LastName;
            }
          
            user.Dob = req.Dob;
            user.Email = req.Email;
            user.PhoneNumber = req.PhoneNumber;

            if(req.ImageUrl != null)
            {
                user.ImageUrl = await _fileStorageService.SaveFile(req.ImageUrl);
            }

            var res = await _userManager.UpdateAsync(user);
            if (res.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUser(UserDeleteRequest req)
        {
            var user = await _userManager.FindByIdAsync(req.Id.ToString());

            if (user == null)
            {
                throw new Exception("User not existed");
            }

            if (user.ImageUrl != null)
            {
                await _fileStorageService.DeleteFileAsync(user.ImageUrl);
            }

            var res = await _userManager.DeleteAsync(user);

            if (res.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}

