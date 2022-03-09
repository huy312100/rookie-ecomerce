using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.CustomerSite.Models;
using eCommerce.CustomerSite.Interfaces;
using eCommerce.CustomerSite.ViewModels;

namespace eCommerce.CustomerSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;

    public HomeController(ILogger<HomeController> logger,IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var product = await _productService.GetTop8Product();
        var model = new HomeVM
        {
            FeaturedProducts = product
        };
        return View(model);
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

