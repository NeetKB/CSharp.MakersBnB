using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakersBnB.Controllers;

public class SessionsController : Controller
{
private readonly ILogger<SessionsController> _logger;

    public SessionsController(ILogger<SessionsController> logger)
    {
        _logger = logger;
    }

    public IActionResult New()
    {
        
        return View();
    }

    [Route("/Sessions")]
    [HttpPost]
    public IActionResult Create(string email, string password) 
    {
        Console.WriteLine("SessionsController.Create is called");
        
        MakersBnBDbContext dbContext = new MakersBnBDbContext();
        
        User? user = dbContext.Users?.Where(user => user.Email == email).FirstOrDefault();
        
        if(user != null && user.Password == password)
        {
            // putting the user's id in their session for later
            HttpContext.Session.SetInt32("user_id", user.ID);
        return new RedirectResult("/Spaces");
        } else {    
            ViewBag.LoginFailed = true;
            ViewBag.email = null;
        return View("New");
        }

    }

}