namespace MakersBnB.Tests;


using MakersBnB.Models;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

public class SpacesTests : PageTest
{
   
    string name = "Test Name";
    string description = "Test Description";
    string bedrooms = "Test Bedroom";
    string price = "Test price";

    string rules = "Test rules";
    
    [SetUp]
    public void CleanUpBefore()
    {
        RemoveTestSpace();
    }

    [TearDown]
    public void CleanUpAfter()
    {
        RemoveTestSpace();
    }

    private void RemoveTestSpace()
    {
        MakersBnBDbContext dbContext = new MakersBnBDbContext();
        var testSpace = dbContext.Spaces?.FirstOrDefault(s => s.Name == name);
        if (testSpace != null)
        {
            dbContext.Spaces?.Remove(testSpace);
            dbContext.SaveChanges();
        }
    }

public async Task SignInUser()
{
    await Page.GotoAsync("http://localhost:5241/Sessions/New");
    await Page.GetByLabel("Email:").FillAsync("lm@email.com");
    await Page.GetByLabel("Password:").FillAsync("Larry123!");       
    await Page.GetByRole(AriaRole.Button).ClickAsync();
}
[Test]// User must provide a name else error message appears
public async Task NewSpacesMustHaveName()
{

    await SignInUser();
    await Page.GotoAsync("http://localhost:5241/Spaces/New"); 
    await Page.GetByLabel("Name: ").FillAsync("");
    await Page.GetByLabel("Description: ").FillAsync(description);
    await Page.GetByLabel("Bedrooms: ").FillAsync(bedrooms);
    await Page.GetByLabel("Price: ").FillAsync(price);
    await Page.GetByLabel("Rules: ").FillAsync("Test");
    await Page.GetByRole(AriaRole.Button, new () { Name = "Submit" }).ClickAsync();

    await Expect(Page.GetByText("You need to provide a Name, Description, number of bedrooms and a price.")).ToBeVisibleAsync();
}

[Test]// User must provide a description else error message appears
public async Task NewSpacesMustHaveDescription()
{

    await SignInUser();
    await Page.GotoAsync("http://localhost:5241/Spaces/New"); 
    await Page.GetByLabel("Name: ").FillAsync(name);
    await Page.GetByLabel("Description: ").FillAsync("");
    await Page.GetByLabel("Bedrooms: ").FillAsync(bedrooms);
    await Page.GetByLabel("Price: ").FillAsync(price);
    await Page.GetByLabel("Rules: ").FillAsync(rules);
    await Page.GetByRole(AriaRole.Button, new () { Name = "Submit" }).ClickAsync();

    await Expect(Page.GetByText("You need to provide a Name, Description, number of bedrooms and a price.")).ToBeVisibleAsync();
}

[Test]// User must provide Bedrooms else error message appears
public async Task NewSpacesMustHaveBedrooms()
{

    await SignInUser();
    await Page.GotoAsync("http://localhost:5241/Spaces/New"); 
    await Page.GetByLabel("Name: ").FillAsync(name);
    await Page.GetByLabel("Description: ").FillAsync(description);
    await Page.GetByLabel("Bedrooms: ").FillAsync("");
    await Page.GetByLabel("Price: ").FillAsync(price);
    await Page.GetByLabel("Rules: ").FillAsync(rules);
    await Page.GetByRole(AriaRole.Button, new () { Name = "Submit" }).ClickAsync();

    await Expect(Page.GetByText("You need to provide a Name, Description, number of bedrooms and a price.")).ToBeVisibleAsync();
}

[Test]// User must provide A Price else error message appears
public async Task NewSpacesMustHavePrice()
{

    await SignInUser();
    await Page.GotoAsync("http://localhost:5241/Spaces/New"); 
    await Page.GetByLabel("Name: ").FillAsync(name);
    await Page.GetByLabel("Description: ").FillAsync(description);
    await Page.GetByLabel("Bedrooms: ").FillAsync(bedrooms);
    await Page.GetByLabel("Price: ").FillAsync("");
    await Page.GetByLabel("Rules: ").FillAsync(rules);
    await Page.GetByRole(AriaRole.Button, new () { Name = "Submit" }).ClickAsync();

    await Expect(Page.GetByText("You need to provide a Name, Description, number of bedrooms and a price.")).ToBeVisibleAsync();
}

[Test] //users can navigate to individual spaces by clicking on a link
public async Task SpacesUsersCanViewSelectedSpace()
{
    await SignInUser();
    await Page.GotoAsync("http://localhost:5241/Spaces/"); 
    await Page.GetByRole(AriaRole.Link, new () { Name = "Click here to view availability of Shed" }).ClickAsync();

    await Expect(Page.GetByText("Search for Availability: ")).ToBeVisibleAsync();
}

[Test] //User can view name, description, price, bedrooms and rules 
public async Task SpacesUsersCanViewAllInformationReASpace(){
    await SignInUser();
    await Page.GotoAsync("http://localhost:5241/Spaces/ViewSpace?spaceId=1"); 
    await Expect(Page.GetByText("Tim")).ToBeVisibleAsync();
}

[Test] // User is able to search dates
public async Task SpacesUsersCanViewSearchDates(){
    await SignInUser();
    await Page.GotoAsync("http://localhost:5241/Spaces/ViewSpace?spaceId=1"); 
    await Expect(Page.GetByLabel("Search for Availability: ")).ToBeVisibleAsync();
}
}