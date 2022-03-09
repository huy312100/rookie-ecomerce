using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.Shared.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.CustomerSite.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var token = await _userService.Login(req);
            return View(token);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}

