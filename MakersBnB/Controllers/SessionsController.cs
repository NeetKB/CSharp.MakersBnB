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
    //SIGNING IN
    public IActionResult Create(string email, string password) 
    {
        Console.WriteLine("SessionsController.Create is called");
        
        MakersBnBDbContext dbContext = new MakersBnBDbContext();
        
        // Where is a method that takes a predicate (a function that returns true or false for each element)
        //  and filters out users that don’t meet the condition.
        User? user = dbContext.Users?.Where(user => user.Email == email).FirstOrDefault();
        // user represents each User object in the collection Users.
        // user.Email is the Email property of the User object.
        // email is the input parameter that you’re trying to match against the Email of each User.
        // So, this will filter all Users whose Email matches the provided email.
        
        if(user != null && user.Password == password)
        {
            // putting the user's id in their session for later
            HttpContext.Session.SetInt32("user_id", user.ID);
            ViewBag.SignOutButton = true;
        return new RedirectResult("/Spaces");
        } else {    
            ViewBag.SignOutButton = false;
            ViewBag.LoginFailed = true;
            ViewBag.email = null;
        return View("New");
        }

    }

    [Route("/Sessions/LogOut")]
    [HttpPost]
    public IActionResult Create()
    {
    return View("/Sessions/New");   
    }

}