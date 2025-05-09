using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace MakersBnB.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    //Get Users/New page 
    public IActionResult New(){
        MakersBnBDbContext dbContext = new MakersBnBDbContext();
        ViewBag.SignOutButton = false;
        return View();
    }

    [Route("/Users")]
    [HttpPost]
    //Post new users
    public IActionResult Create(User user)
    {
        ViewBag.SignOutButton = false;
        MakersBnBDbContext dbContext = new MakersBnBDbContext();
        Regex validateGuidRegex = new Regex("^(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
        Console.WriteLine(validateGuidRegex.IsMatch("-Secr3t."));  // prints True

        bool UsernameExists = dbContext.Users?.Any(u => u.Username == user.Username) ?? false ;
        bool EmailExists = dbContext.Users?.Any(u => u.Email == user.Email) ?? false ;
        Console.WriteLine($"UsernameExists is {UsernameExists}");
        if(UsernameExists || EmailExists){
            ViewBag.UsernameExists = true;
            ViewBag.EmailExists = true;
            return View("New");
        }else if(user.Username == null){
            ViewBag.UsernameBlank = true;
            return View("New");
        } else if(user.Email == null){
            ViewBag.EmailBlank = true;
            return View("New");
        }else if(user.Password == null || validateGuidRegex.IsMatch(user.Password) == false){
            ViewBag.PasswordBlank = true;
            return View("New");
        }else{
            dbContext.Users?.Add(user);
            dbContext.SaveChanges();

            return new RedirectResult("/Sessions/New");}    
    }
}