using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.BackendApi.Interfaces;
using eCommerce.Shared.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest req)
        {
            var res = await _userService.Register(req);

            if (!res)
            {
                return BadRequest("Register unsuccessful");
            }
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUser()
        {
            var res = await _userService.GetAllUser();
            if(res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var res = await _userService.GetUserById(id);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
    }
}

