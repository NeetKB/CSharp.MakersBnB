using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using MakersBnB.ActionFilters;

namespace MakersBnB.Controllers;

public class SpacesController : Controller
{
    private readonly ILogger<SpacesController> _logger;

    public SpacesController(ILogger<SpacesController> logger)
    {
        _logger = logger;
    }

    // will handle `GET "/Spaces"`
    [ServiceFilter(typeof(AuthenticationFilter))]
    public IActionResult Index()
    {
        using var dbContext = new MakersBnBDbContext();
        var spaces = dbContext.Spaces?.ToList();
        return View(spaces); // passing directly as model
        //Alternative way to display:

    //     MakersBnBDbContext dbContext = new MakersBnBDbContext();
    //     List<Space> spaces = dbContext.Spaces.ToList();

    //    foreach (var space in spaces)
    // {
    //     Console.WriteLine($"Name: {space.Name}, Description: {space.Description}, Price: {space.Price}");
       
    // };

    // ViewBag.Spaces = spaces;

    //     return View();
    }
    [ServiceFilter(typeof(AuthenticationFilter))]
    public IActionResult New()
    {
        return View();
    }

// In this case we need a custom route mapping
// we also need to specify that we're handling a POST request
// to differentiate from Index(), which handles 'GET "/spaces"'
[Route("/Spaces")]
[HttpPost]
[ServiceFilter(typeof(AuthenticationFilter))]
public IActionResult Create(Space space)
{   
  MakersBnBDbContext dbContext = new MakersBnBDbContext();
  // Here's where we finally use the dbContext
  dbContext.Spaces?.Add(space);
  dbContext.SaveChanges();

  // redirect to "/Spaces"
  return new RedirectResult("/Spaces");
}

}
