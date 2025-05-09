namespace MakersBnB.Tests;

using System.Text.RegularExpressions;

using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

public class Tests : PageTest
{

    [Test]
    public async Task IndexpageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        // go to the MakersBnB Index page
        await Page.GotoAsync("http://localhost:5241");

        // expect the page title to contain "Index Page - MakersBnB"
        await Expect(Page).ToHaveTitleAsync(new Regex("Index Page - MakersBnB"));
    }

    [Test]
    public async Task HomePageIncludesWelcomeMessage() {
     await Page.GotoAsync("http://localhost:5241");

     await Expect(Page.GetByText("Welcome to MakersBnB!")).ToBeVisibleAsync();
 }

    [Test]
    public async Task GetStartedLink()
    {
        await Page.GotoAsync("http://localhost:5241");

        // Click the Privacy link.
        await Page.GetByRole(AriaRole.Link, new() { Name = "Privacy" }).ClickAsync();

        // Expects page to have a heading with the name of Privacy Policy.
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Privacy Policy" })).ToBeVisibleAsync();
    } 

    [Test]
    public async Task CheckReviewsAreRendered(){
        await Page.GotoAsync("http://localhost:5241");

        await Expect(Page.GetByText("Great rooms")).ToBeVisibleAsync();
    }

    [Test]
    public async Task CheckListItemIsVisibleOnPrivacyPage(){
        await Page.GotoAsync("http://localhost:5241/Home/Privacy");
        
        await Expect(Page
        .GetByRole(AriaRole.Listitem)
        .Filter(new(){HasText = "mandip"}))
        .ToBeVisibleAsync();   
    }

    [Test]
    public async Task CheckTitleAttributeIsPresent(){
        await Page.GotoAsync("http://localhost:5241/Home/Privacy");

        await Expect(Page.GetByTitle("information")).ToHaveTextAsync("Privacy policy details go here");
    }

    [Test] // Check that Home page has sign in link
    public async Task HomePageHasSignInButton()
    {
        await Page.GotoAsync("http://localhost:5241/");
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Sign In" })).ToBeVisibleAsync();
    }

    [Test] // Check that Home page has sign up link
    public async Task HomePageHasSignUpButton()
    {
        await Page.GotoAsync("http://localhost:5241/");
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Sign Up" })).ToBeVisibleAsync();
    }

    [Test]//check that Sign out link is in Navbar if user signed in
    public async Task SignOutLinkInNavBarIfSignedIn()
    {
         await Page.GotoAsync("http://localhost:5241/Sessions/New");
        await Page.GetByLabel("Email:").FillAsync("lm@email.com");
        await Page.GetByLabel("Password:").FillAsync("Larry123!");
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Page.GotoAsync("http://localhost:5241/Spaces");
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Sign Out" })).ToBeVisibleAsync();

    }    
}
