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
        ViewBag.SignOutButton = true;
        //Alternative way to display:
        // using var dbContext = new MakersBnBDbContext();
        // var spaces = dbContext.Spaces?.ToList();
        // return View(spaces); // passing directly as model
        

        MakersBnBDbContext dbContext = new MakersBnBDbContext();
        List<Space>? spaces = dbContext?.Spaces?.ToList();

       foreach (var space in spaces)
        {
            Console.WriteLine($"ID: {space.Id}, Name: {space.Name}, Description: {space.Description}, Price: {space.Price}, Bedrooms: {space.Bedrooms}, Rules: {space.Rules}");
        
        };

        ViewBag.Spaces = spaces;

        return View();
    }

    [ServiceFilter(typeof(AuthenticationFilter))]
    //Get Spaces/New page
    public IActionResult New()
    {
        ViewBag.SignOutButton = true;
        return View();
    }

// In this case we need a custom route mapping
// we also need to specify that we're handling a POST request
// to differentiate from Index(), which handles 'GET "/spaces"'
[Route("/Spaces/New")]
[HttpPost]
[ServiceFilter(typeof(AuthenticationFilter))]
//Post new spaces
public IActionResult Create(Space space)
{   
    ViewBag.SignOutButton = true;
    ViewBag.NoName = false;
    MakersBnBDbContext dbContext = new MakersBnBDbContext();
    // Here's where we finally use the dbContext
    
    if (space.Name == null || space.Description == null || space.Bedrooms == null || space.Price == null){
        ViewBag.ShowError = true;
        return View("New");
    }else{
        
        dbContext.Spaces?.Add(space);
        dbContext.SaveChanges();
        return new RedirectResult("/Spaces");
    }    
}

//GET SPACES/VIEWSPACE?Id={id}
public IActionResult ViewSpace(int spaceId)
{
    MakersBnBDbContext dbContext = new MakersBnBDbContext();
    
    Console.WriteLine($"SspaceId is {spaceId}");
    Space? space = dbContext.Spaces?.Where(s => s.Id == spaceId).FirstOrDefault();

    if (space ==null){
        return View();
    }else{
        ViewBag.Name = space?.Name;
        ViewBag.Description = space?.Description;
        ViewBag.Bedroom = space?.Bedrooms;
        ViewBag.Price = space?.Price;
        ViewBag.Rules = space?.Rules;
        return View();
    }
    
    
}

}
