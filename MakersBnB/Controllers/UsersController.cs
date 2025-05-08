using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakersBnB.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    public IActionResult New(){
        MakersBnBDbContext dbContext = new MakersBnBDbContext();

        return View();
    }

    [Route("/Users")]
    [HttpPost]
    public IActionResult Create(User user)
    {
        MakersBnBDbContext dbContext = new MakersBnBDbContext();
        dbContext.Users?.Add(user);
        dbContext.SaveChanges();

        return new RedirectResult("/Spaces");
    }
}