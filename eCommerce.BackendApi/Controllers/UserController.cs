using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.BackendApi.Interfaces;
using eCommerce.Shared.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.BackendApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest req)
        {
            var token = await _userService.Login(req);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Username or password is incorrect");
            }
            return Ok(new { token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest req)
        {
            var res = await _userService.Register(req);

            if (!res)
            {
                return BadRequest("Register unsuccessful");
            }
            return Ok();
        }
    }
}

