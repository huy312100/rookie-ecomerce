using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.BackendApi.Interfaces;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var token = await _userService.Login(req,"Customer");

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Username or password is incorrect");
            }
            return Ok(token);
        }

        [HttpPost("admin/login")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminLogin([FromBody] LoginRequest req)
        {
            var token = await _userService.Login(req, "Admin");

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Username or password is incorrect");
            }
            return Ok(token);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
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

        [HttpPost("paging")]
        public async Task<IActionResult> GetUsersPaging([FromQuery] PagingRequest request)
        {
            var users = await _userService.GetUsersPaging(request);
            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest req)
        {
            var res = await _userService.UpdateUser(req);
            if (!res)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(UserDeleteRequest req)
        {
            var res = await _userService.DeleteUser(req);
            if (!res)
            {
                return BadRequest("Delete user unsuccessful");
            }
            return Ok(res);
        }
    }
}

