using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.CustomerSite.Controllers
{
    public class AuthenController : Controller
    {
        private readonly ILogger<AuthenController> _logger;

        public AuthenController(ILogger<AuthenController> logger)
        {
            _logger = logger;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}

