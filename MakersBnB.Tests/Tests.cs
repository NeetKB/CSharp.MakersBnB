namespace MakersBnB.Tests;

using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

public class Tests : PageTest
{
    // the following method is a test
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

}
