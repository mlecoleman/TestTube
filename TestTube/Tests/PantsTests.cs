using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using FluentAssertions.Execution;
using TestTube.PageObjectModels;
using TestTube.Tests;
using AngleSharp.Dom;
using System.Runtime.Intrinsics.X86;
using System.Diagnostics.Metrics;
using OpenQA.Selenium.Interactions;
using System.Linq;

namespace TestTube

{
    public sealed class PantsTests : BaseTest
    {
        private readonly ITestOutputHelper testOutputHelper;
        PantsDotOrgPages _pantsDotOrg;
        TwitterPage _twitterPage;
        WordpressLoginPage _wordpressLoginPage;
        PantsHolidaysPage _pantsHolidaysPage;
        FileUploaderPage _fileUploaderPage;
        RedAntsPantsPage _redAntsPantsCafe;
        TheArtOfPantsPages _artOfPantsPages;

        public PantsTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;

            PantsDotOrgPages pantsDotOrg = new PantsDotOrgPages();
            _pantsDotOrg = pantsDotOrg;

            TwitterPage twitterPage = new TwitterPage();
            _twitterPage = twitterPage;

            WordpressLoginPage wordpressLoginPage = new WordpressLoginPage();
            _wordpressLoginPage = wordpressLoginPage;

            PantsHolidaysPage pantsHolidaysPage = new PantsHolidaysPage(Driver);
            _pantsHolidaysPage = pantsHolidaysPage;

            FileUploaderPage fileUploaderPage = new FileUploaderPage(Driver);
            _fileUploaderPage = fileUploaderPage;

            RedAntsPantsPage redAntsPantsPage = new RedAntsPantsPage(Driver);
            _redAntsPantsCafe = redAntsPantsPage;

            TheArtOfPantsPages theArtOfPantsPages = new TheArtOfPantsPages(Driver);
            _artOfPantsPages = theArtOfPantsPages;
        }

        // Test 1
        // Meets Requirement:
        // Include at least one test that performs an action that opens a new tab or window, and
        // then continues the test in that new tab or window.
        [Fact]
        public void PantsDotOrgTwitterButton()
        {
            //testOutputHelper.WriteLine("example");
            // Arrange - Navigate to wordpress site pants.org
            Driver.Navigate().GoToUrl(_pantsDotOrg.pantsDotOrgUrl);

            // Act - Click on the Twitter Button/Link and wait for the new tab to be fully loaded
            Driver.FindElement(_pantsDotOrg.TwitterButton).Click();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            _wait.Until(c => Driver.FindElement(_twitterPage.gitTwitterUserUrlLink).Displayed);

            // Assert - 2 Windows are open and the new window has the expected Twitter title & URL
            using (new AssertionScope())
            {
                Driver.WindowHandles.Should().HaveCount(2);
                Driver.Url.Should().Be(_twitterPage.twitterUrl);
                Driver.Title.Should().Be(_twitterPage.twitterTitle);
            }
        }

        // Test 2
        [Fact]
        public void NavigateToAboutMeTest()
        {
            // Arrange - Navigate to pants.org 
            Driver.Navigate().GoToUrl(_pantsDotOrg.pantsDotOrgUrl);

            // Act - Click on About Me in the nav bar
            Driver.FindElement(_pantsDotOrg.NavBarAboutMe).Click();

            // Assert - The web address is correct and the About Me header is present
            using (new AssertionScope())
            {
                Driver.Url.Should().Be(_pantsDotOrg.pantsAboutMeUrl);
                Driver.FindElement(_pantsDotOrg.AboutMeHeader).Displayed.Should().BeTrue();
            }
        }

        // Test 3
        [Fact]
        public void PantsSearchBarTest()
        {
            // Arrange - Navigate to pants.org 
            Driver.Navigate().GoToUrl(_pantsDotOrg.pantsDotOrgUrl);

            // Act - Use the page search bar to search for Pants
            Driver.FindElement(_pantsDotOrg.SearchBar).SendKeys("Pants");
            Driver.FindElement(_pantsDotOrg.SearchBar).SendKeys(Keys.Return);


            // Assert - The header and url contain the search query (Pants)
            using (new AssertionScope())
            {
                Driver.Url.Should().Contain("Pants");
                Driver.FindElement(_pantsDotOrg.SearchResultsHeader).Displayed.Should().BeTrue();
            }
        }

        // Test 4
        // Meets Requirement:
        // Negative Test
        [Fact]
        public void WordpressFailAtProvingHumanityNegativeTest()
        {
            // Arrange - Navigate to pants.org (I know I should probably just navigate directly to the
            // Wordpress login page, but pants.org is funnier)
            Driver.Navigate().GoToUrl(_pantsDotOrg.pantsDotOrgUrl);

            // Act - Click on Wordpress Login link and attempt to login without proving Humanity 
            Driver.FindElement(_pantsDotOrg.wordpressLoginLink).Click();
            Driver.FindElement(_wordpressLoginPage.emailAddressInput).SendKeys("pants@example.com");
            Driver.FindElement(_wordpressLoginPage.passwordInput).SendKeys("pantsdotorgpassword");
            Driver.FindElement(_wordpressLoginPage.proveHumanityInput).SendKeys("");
            Driver.FindElement(_wordpressLoginPage.loginButton).Click();
            _wait.Until(c => Driver.FindElement(_wordpressLoginPage.notHumanErrorPage).Displayed);

            // Assert - Logging in without proving Humanity loads error page with expected error message
            using (new AssertionScope())
            {
                Driver.FindElement(_wordpressLoginPage.notHumanErrorPage).Displayed.Should().BeTrue();
                Driver.Title.Should().Be(_wordpressLoginPage.wordpressErrorPageTitle);
            }
        }

        // Test 5
        // Meets Requirement:
        // A date picker
        [Fact]
        public void PantsHolidaysCalendarPicker()
        {
            // Arrange - Navigate to url for May 5th 2023 - No Pants Day
            Driver.Navigate().GoToUrl(_pantsHolidaysPage.noPantsDayUrl);

            // Act - Select Date July 27th - Take your Pants for a Walk Day
            _pantsHolidaysPage.chooseJuly2023();
            Driver.FindElement(_pantsHolidaysPage.july27th).Click();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());


            // Assert - Take your Pants for a Walk Day Url and Page elements are displayed
            using (new AssertionScope())
            {
                Driver.FindElement(_pantsHolidaysPage.walkYourPantsHolidayHeader).Displayed.Should().BeTrue();
                Driver.Url.Should().Be(_pantsHolidaysPage.takeYourPantsForAWalkDayUrl);
            }
        }

        // Test 6
        // Meets Requirement:
        // A file upload
        [Fact]
        public void UploadImageOfPantsTest()
        {
            // Arrange - Navigate to url for file uploader
            Driver.Navigate().GoToUrl(_fileUploaderPage.FileUploaderUrl);

            // Act - Choose ann image and upload it
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            Driver.FindElement(_fileUploaderPage.ChooseFileButton).SendKeys(path + "/Upload/PantsJacket.jpg");
            Driver.FindElement(_fileUploaderPage.UploadButton).Click();

            // Assert - Image uploaded panel is present with file text & File Uploaded! header is displayed
            using (new AssertionScope())
            {
                Driver.FindElement(_fileUploaderPage.UploadedFilesPanel).Displayed.Should().BeTrue();
                Driver.FindElement(_fileUploaderPage.UploadedFilesPanel).Text.Should().Be("PantsJacket.jpg");
                Driver.FindElement(_fileUploaderPage.FileUploadedHeader).Displayed.Should().BeTrue();
            }
        }

        // Test 7
        // Meets Requirement:
        // A hover-over or tooltip
        [Fact]
        public void TestAnthillHoverOverDropdownMenu()
        {
            // Arrange - Navigate to url for Red Ants Pants
            Driver.Navigate().GoToUrl(_redAntsPantsCafe.RedAntsPantsUrl);

            // Act - Hover over ANTHILL item in nav bar
            _actions.MoveToElement(Driver.FindElement(_redAntsPantsCafe.AnthillNavItem)).Perform();
            _wait.Until(c => Driver.FindElement(_redAntsPantsCafe.PantsPicsMenuItem).Displayed);

            // Assert - The contributions tooltip appears for No Pants Day (May 5th)
            Driver.FindElement(_redAntsPantsCafe.PantsPicsMenuItem).Displayed.Should().BeTrue();
        }

        // Test 8
        [Fact]
        public void TestExpectedNumberOfArtCards()
        {
            // Arrange - Navigate to url for The Art Of Pants
            // Act - View The Art Of Pants Page
            Driver.Navigate().GoToUrl(_artOfPantsPages.TheArtOfPantsUrl);

            // Assert - There should only be 9 Featured Pants Art Cards and 6 Shop More Pants Cards
            using (new AssertionScope())
            {
                Driver.FindElements(_artOfPantsPages.PantsArtCards).Should().HaveCount(9);
                Driver.FindElements(_artOfPantsPages.ShopMorePantsCards).Should().HaveCount(6);
            }
        }

        // Test 9
        [Fact]
        public void TestAddingGreetingCardToShoppingCart()
        {
            // Arrange - Navigate to url for The Art Of Pants Greeting Cards page
            Driver.Navigate().GoToUrl(_artOfPantsPages.TheArtOfPantsGreetingCardsUrl);

            // Act - Click on first greeting card on greeting cards page
            _artOfPantsPages.ChooseFirstGreetingCard();
            Driver.FindElement(_artOfPantsPages.AddToCartButton).Click();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.CartNotification).Displayed);

            // Assert - Item added to cart notification appears and shows confirmation header
            using (new AssertionScope())
            {
                Driver.FindElement(_artOfPantsPages.CartNotification).Displayed.Should().BeTrue();
                Driver.FindElement(_artOfPantsPages.ItemAddedToCartHeader).Text.Should().Contain("Item added to your cart");
            }
        }

        // Test 10
        [Fact]
        public void TestTopNavBarInfoDropdownOptions()
        {
            // Arrange - Navigate to url for the art of pants
            Driver.Navigate().GoToUrl(_artOfPantsPages.TheArtOfPantsUrl);

            // Act - Click on the Info dropdown
            Driver.FindElement(_artOfPantsPages.InfoTopNavItem).Click();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.InfoOptions).Displayed);
            List<string> infoOptions= new List<string>(Driver.FindElements(_artOfPantsPages.InfoOptions).Select(iw => iw.Text));

            // Assert - There are only three items that have the expected text and are in the expected order
            Driver.FindElements(_artOfPantsPages.InfoOptions).Should().HaveCount(3);
            infoOptions.Should().ContainInConsecutiveOrder("Contact", "Shipping Info", "FAQ");
        }

        // Test 11
        [Fact]
        public void TestSearchBarIcon()
        {
            // Arrange - Navigate to url for the art of pants
            Driver.Navigate().GoToUrl(_artOfPantsPages.TheArtOfPantsUrl);

            // Act - Click on search icon
            Driver.FindElement(_artOfPantsPages.SearchIcon).Click();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.PantsFinder).Displayed);


            // Assert - The Pants Finder should be displayed
            Driver.FindElement(_artOfPantsPages.PantsFinder).Displayed.Should().BeTrue();
        }

        // Test 12
        [Fact]
        public void TestRemovingItemFromShoppingCart()
        {
            // Arrange - Add an item to the shopping cart and navigate to shopping cart
            //Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(_artOfPantsPages.TheArtOfPantsGreetingCardsUrl);
            _artOfPantsPages.ChooseFirstGreetingCard();
            Driver.FindElement(_artOfPantsPages.AddToCartButton).Click();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.CartNotification).Displayed);
            Driver.FindElement(_artOfPantsPages.ViewMyCartLink).Click();

            // Act - Remove Item from Shopping Cart
            Driver.FindElement(_artOfPantsPages.RemoveItem).Click();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.NoPantsHeader).Displayed);

            // Assert - No Pants header is displayed and the Page Title is correct
            using (new AssertionScope())
            {
                Driver.FindElement(_artOfPantsPages.NoPantsHeader).Displayed.Should().BeTrue();
                Driver.Title.Should().Be("Your Shopping Cart – The Art of Pants");

            }
        }

        //// Test 13
        //[Fact]
        //public void Test6()
        //{
        //    // Arrange - Navigate to url for pants github
        //    //Driver.Manage().Window.Maximize();
        //    Driver.Navigate().GoToUrl(_redAntsPantsCafe.RedAntsPantsUrl);
        //    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        //    //wait.Until(c => Driver.FindElement(_redAntsPantsCafe.About).Displayed);


        //    // Act - Hover over No Pants Day (May 5th)
        //    //_actions.MoveToElement(Driver.FindElement(_pantsGithub.May5thSquare)).Perform();
        //    //_actions.ScrollToElement(Driver.FindElement(_antsPantsCafe.About)).Perform();
        //    //_actions.MoveToElement(Driver.FindElement(_redAntsPantsCafe.About)).Perform();

        //    // Assert - The contributions tooltip appears for No Pants Day (May 5th)
        //    using (new AssertionScope())
        //    {
        //        Driver.FindElement(_redAntsPantsCafe.ContributionsTooltip).Displayed.Should().BeTrue();
        //        //Driver.FindElement(_fileUploaderPage.UploadedFilesPanel).Text.Should().Be("Pants.jpg");
        //        //Driver.FindElement(_fileUploaderPage.FileUploadedHeader).Displayed.Should().BeTrue();
        //    }
        //}

        //// Test 14
        //[Fact]
        //public void Test7()
        //{
        //    // Arrange - Navigate to url for pants github
        //    //Driver.Manage().Window.Maximize();
        //    Driver.Navigate().GoToUrl(_redAntsPantsCafe.RedAntsPantsUrl);
        //    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        //    //wait.Until(c => Driver.FindElement(_redAntsPantsCafe.About).Displayed);


        //    // Act - Hover over No Pants Day (May 5th)
        //    //_actions.MoveToElement(Driver.FindElement(_pantsGithub.May5thSquare)).Perform();
        //    //_actions.ScrollToElement(Driver.FindElement(_antsPantsCafe.About)).Perform();
        //    //_actions.MoveToElement(Driver.FindElement(_redAntsPantsCafe.About)).Perform();

        //    // Assert - The contributions tooltip appears for No Pants Day (May 5th)
        //    using (new AssertionScope())
        //    {
        //        Driver.FindElement(_redAntsPantsCafe.ContributionsTooltip).Displayed.Should().BeTrue();
        //        //Driver.FindElement(_fileUploaderPage.UploadedFilesPanel).Text.Should().Be("Pants.jpg");
        //        //Driver.FindElement(_fileUploaderPage.FileUploadedHeader).Displayed.Should().BeTrue();
        //    }
        //}

        //// Test 15
        //[Fact]
        //public void Test8()
        //{
        //    // Arrange - Navigate to url for pants github
        //    //Driver.Manage().Window.Maximize();
        //    Driver.Navigate().GoToUrl(_redAntsPantsCafe.RedAntsPantsUrl);
        //    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        //    //wait.Until(c => Driver.FindElement(_redAntsPantsCafe.About).Displayed);


        //    // Act - Hover over No Pants Day (May 5th)
        //    //_actions.MoveToElement(Driver.FindElement(_pantsGithub.May5thSquare)).Perform();
        //    //_actions.ScrollToElement(Driver.FindElement(_antsPantsCafe.About)).Perform();
        //    //_actions.MoveToElement(Driver.FindElement(_redAntsPantsCafe.About)).Perform();

        //    // Assert - The contributions tooltip appears for No Pants Day (May 5th)
        //    using (new AssertionScope())
        //    {
        //        Driver.FindElement(_redAntsPantsCafe.ContributionsTooltip).Displayed.Should().BeTrue();
        //        //Driver.FindElement(_fileUploaderPage.UploadedFilesPanel).Text.Should().Be("Pants.jpg");
        //        //Driver.FindElement(_fileUploaderPage.FileUploadedHeader).Displayed.Should().BeTrue();
        //    }
        //}
    }
}

