namespace MakersBnB.Tests;

using System.Text.RegularExpressions;
using MakersBnB.Models;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Faker;
public class AuthTests : PageTest
{
    string username = Faker.Name.FullName();
    string email = Internet.Email();
    string password = "Secret123!";


    [SetUp]
    public void CleanUpBefore()
    {
        RemoveTestUser();
    }

    [TearDown]
    public void CleanUpAfter()
    {
        RemoveTestUser();
    }

    private void RemoveTestUser()
    {
        MakersBnBDbContext dbContext = new MakersBnBDbContext();
        var testUser = dbContext.Users?.FirstOrDefault(u => u.Email == email);
        if (testUser != null)
        {
            dbContext.Users?.Remove(testUser);
            dbContext.SaveChanges();
        }
    }
    
    [Test]
    public async Task SigningInWithCorrectCredentials()
    {
        await Page.GotoAsync("http://localhost:5241/Users/New");
        await Page.GetByLabel("Password:").FillAsync("Larry123!");
        await Page.GetByLabel("Username:").FillAsync("Larry");
        await Page.GetByLabel("Email:").FillAsync("lm@email.com");
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Page.GotoAsync("http://localhost:5241/Sessions/New");
        await Page.GetByLabel("Email:").FillAsync("lm@email.com");
        await Page.GetByLabel("Password:").FillAsync("Larry123!");
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        // you will need to update `Spaces/Index` to make this pass
        await Expect(Page).ToHaveTitleAsync(new Regex("Spaces Page - MakersBnB"));
        
    }
    [Test]
     public async Task SigningInWithWrongCredentials()
    {
        
        await Page.GotoAsync("http://localhost:5241/Sessions/New");
        await Page.GetByLabel("Email:").FillAsync("hh@email.com");
        await Page.GetByLabel("Password:").FillAsync(password);
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Expect(Page.GetByText("Cannot find email address provided. Please register your details first.")).ToBeVisibleAsync();
    }

    [Test] // Check that if a username is already in the database - cannot sign in - error message given
    public async Task RegisteringWithUsernameThatsAlreadyInDb()
    {
        await Page.GotoAsync("http://localhost:5241/Users/New");
        await Page.GetByLabel("Username:").FillAsync("Avnita");
        await Page.GetByLabel("Email:").FillAsync(email);
        await Page.GetByLabel("Password:").FillAsync(password);
        await Page.GetByRole(AriaRole.Button).ClickAsync();
        
        await Page.GotoAsync("http://localhost:5241/Users/New");
        await Page.GetByLabel("Username:").FillAsync("Avnita");
        await Page.GetByLabel("Email:").FillAsync(email);
        await Page.GetByLabel("Password:").FillAsync(password);
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Expect(Page.GetByText("Username  or Email already exists. Please choose a different username or email")).ToBeVisibleAsync();

    }

    [Test] // Check that if a email is in the database - cannot sign in - error message given
    public async Task RegisteringWithEmailAlreadyInDB()
    {
        await Page.GotoAsync("http://localhost:5241/Users/New");
        await Page.GetByLabel("Username:").FillAsync(username);
        await Page.GetByLabel("Email:").FillAsync("ab@test.com");
        await Page.GetByLabel("Password:").FillAsync(password);
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Expect(Page.GetByText("Username  or Email already exists. Please choose a different username or email")).ToBeVisibleAsync();
    }
    [Test]//check that username is always provided - else they cannot sign in and error appears
    public async Task RegisteringWithoutUsername()
    {
        await Page.GotoAsync("http://localhost:5241/Users/New");
        await Page.GetByLabel("Username:").FillAsync("");
        await Page.GetByLabel("Email:").FillAsync(email);
        await Page.GetByLabel("Password:").FillAsync(password);
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Expect(Page.GetByText("You need to provide a valid username.")).ToBeVisibleAsync();
    }

    [Test]//check that email is always provided - else they cannot sign in and error appears
    public async Task RegisteringWithoutEmail()
    {
        await Page.GotoAsync("http://localhost:5241/Users/New");
        await Page.GetByLabel("Username:").FillAsync(username);
        await Page.GetByLabel("Email:").FillAsync("");
        await Page.GetByLabel("Password:").FillAsync(password);
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Expect(Page.GetByText("You need to provide a valid email address.")).ToBeVisibleAsync();
    }

    [Test]//check that password is always provided - else they cannot sign in and error appears
    public async Task RegisteringWithoutPassword()
    {
        await Page.GotoAsync("http://localhost:5241/Users/New");
        await Page.GetByLabel("Username:").FillAsync(username);
        await Page.GetByLabel("Email:").FillAsync(email);
        await Page.GetByLabel("Password:").FillAsync("");
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Expect(Page.GetByText("You need to provide a valid password. It must be longer than 8 characters, one number and have special characters")).ToBeVisibleAsync();
    }

    [Test]//check that password has more than 8 chacarters and special characters - else a warning is given
    
    public async Task RegisteringWithInvalidPassword()
    {
        await Page.GotoAsync("http://localhost:5241/Users/New");
        await Page.GetByLabel("Username:").FillAsync(username);
        await Page.GetByLabel("Email:").FillAsync(email);
        await Page.GetByLabel("Password:").FillAsync("ere");
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Expect(Page.GetByText("You need to provide a valid password. It must be longer than 8 characters, one number and have special characters.")).ToBeVisibleAsync();
    }
    
}
