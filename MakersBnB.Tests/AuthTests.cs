namespace MakersBnB.Tests;

using System.Text.RegularExpressions;
using MakersBnB.Models;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

public class AuthTests : PageTest
{

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
        var testUser = dbContext.Users?.FirstOrDefault(u => u.Email == "email@email.com");
        if (testUser != null)
        {
            dbContext.Users?.Remove(testUser);
            dbContext.SaveChanges();
        }
    }
    [Test]
    public async Task SigningInWithCorrectCredentials()
    {
        // we need to create a user first
        // you might need to tweak this to work with your sign up form
        await Page.GotoAsync("http://localhost:5241/Users/New");
        await Page.GetByLabel("Password:").FillAsync("Secret123!");
        await Page.GetByLabel("Username:").FillAsync("username");
        await Page.GetByLabel("Email:").FillAsync("email@email.com");
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        // signing in - will fail initially!
        await Page.GotoAsync("http://localhost:5241/Sessions/New");
        await Page.GetByLabel("Email:").FillAsync("email@email.com");
        await Page.GetByLabel("Password:").FillAsync("Secret123!");
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        // you will need to update `Spaces/Index` to make this pass
        await Expect(Page).ToHaveTitleAsync(new Regex("Spaces Page - MakersBnB"));
        
    }
    [Test]
     public async Task SigningInWithInCorrectCredentials()
    {
        
        await Page.GotoAsync("http://localhost:5241/Sessions/New");
        await Page.GetByLabel("Email:").FillAsync("newemail@email.com");
        await Page.GetByLabel("Password:").FillAsync("SecRet123!");
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Expect(Page.GetByText("Cannot find email address provided. Please register your details first.")).ToBeVisibleAsync();


    }
}
