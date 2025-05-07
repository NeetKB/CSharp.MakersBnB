using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MakersBnB.Models;
using Microsoft.Extensions.WebEncoders.Testing;

namespace MakersBnB.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    // he ILogger<T> is a built-in logging interface in ASP.NET Core,
    //  used for logging messages related to the application's behavior.
    // used to log messages, errors, or any information you want to track
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    // used for dependency injection, where the ILogger<HomeController> instance 
    // is passed in by the ASP.NET Core framework when the controller is created. 
    // This allows you to log messages within the controller.

    public IActionResult Index()
    // IActionResult is a standard return type for controller actions in ASP.NET Core MVC.
    {
        return View();
    }

    public IActionResult Privacy()
    {
        ViewBag.Names = new string[4] {"tina", "mandip", "sulemon", "jordi"};

        return View();
    }

    public IActionResult Team()
    {
        return View();
    }

    public IActionResult contactus()
    {
        ViewBag.Email = "tester@test.com";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // ResponseCache] attribute is used to control caching for the Error page. 
    //here caching is disabled
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // creates a new ErrorViewModel, which holds information about the current error, including a RequestId. 
        // This is useful for identifying which request caused the error (for debugging purposes).
        // The RequestId is set to the current activity's ID (Activity.Current?.Id) 
        // or the TraceIdentifier from HttpContext if the activity ID is not available.
    }
}
