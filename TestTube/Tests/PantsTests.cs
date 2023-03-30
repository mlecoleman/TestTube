using OpenQA.Selenium;
using Xunit.Abstractions;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using FluentAssertions.Execution;
using TestTube.PageObjectModels;
using TestTube.Tests;

namespace TestTube

{
    public sealed class PantsTests : BaseTest
    {
        private readonly ITestOutputHelper testOutputHelper;
        PantsDotOrgPages _pantsDotOrg;
        TwitterPage _twitterPage;
        WordpressLoginPage _wordpressLoginPage;
        PantsHolidaysPages _pantsHolidaysPage;
        FileUploaderPage _fileUploaderPage;
        RedAntsPantsPage _redAntsPantsPage;
        TheArtOfPantsPages _artOfPantsPages;

        public PantsTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;

            PantsDotOrgPages pantsDotOrg = new PantsDotOrgPages(Driver);
            _pantsDotOrg = pantsDotOrg;

            TwitterPage twitterPage = new TwitterPage(Driver);
            _twitterPage = twitterPage;

            WordpressLoginPage wordpressLoginPage = new WordpressLoginPage(Driver);
            _wordpressLoginPage = wordpressLoginPage;

            PantsHolidaysPages pantsHolidaysPage = new PantsHolidaysPages(Driver);
            _pantsHolidaysPage = pantsHolidaysPage;

            FileUploaderPage fileUploaderPage = new FileUploaderPage(Driver);
            _fileUploaderPage = fileUploaderPage;

            RedAntsPantsPage redAntsPantsPage = new RedAntsPantsPage(Driver);
            _redAntsPantsPage = redAntsPantsPage;

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
            _pantsDotOrg.NavigateToPantsDotOrg();

            // Act - Click on the Twitter Button/Link and wait for the new tab to be fully loaded
            _pantsDotOrg.ClickOnTwitterButton();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            _wait.Until(c => Driver.FindElement(_twitterPage.TwitterUserUrlLink).Displayed);

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
        public void TestPagination()
        {
            // Arrange - Navigate to wordpress site pants.org
            _pantsDotOrg.NavigateToPantsDotOrg();
            //wait.Until(c => Driver.FindElement(_redAntsPantsCafe.About).Displayed);


            // Act - Click "3" in pagination
            _pantsDotOrg.ClickOnPage3InPagination();
            _wait.Until(c => Driver.FindElement(_pantsDotOrg.PaginationCurrentPage).Displayed);


            // Assert - The Url contains page 3 & current Pagination should be 3
            using (new AssertionScope())
            {
                Driver.Url.Should().Contain("page/3");
                Driver.FindElement(_pantsDotOrg.PaginationCurrentPage).Text.Should().Be("3");
            }
        }

        // Test 3
        [Fact]
        public void TestSiteTitleRedirectsToHomePage()
        {
            // Arrange - Navigate to url for pants github
            _pantsDotOrg.NavigateToPantsDotOrg();

            // Act - Click on site Title
            _pantsDotOrg.ClickOnPageTitle();

            // Assert - Clickon the title redirects to the Home Page
            Driver.Url.Should().Be(_pantsDotOrg.pantsDotOrgUrl);
        }

        // Test 4
        [Fact]
        public void NavigateToAboutMeTest()
        {
            // Arrange - Navigate to pants.org
            _pantsDotOrg.NavigateToPantsDotOrg();

            // Act - Click on About Me in the nav bar
            _wait.Until(c => Driver.FindElement(_pantsDotOrg.NavBarAboutMe).Displayed);
            Driver.FindElement(_pantsDotOrg.NavBarAboutMe).Click();
            _wait.Until(c => Driver.FindElement(_pantsDotOrg.AboutMeHeader).Displayed);


            // Assert - The web address is correct and the About Me header is present
            using (new AssertionScope())
            {
                Driver.Url.Should().Be(_pantsDotOrg.pantsAboutMeUrl);
                Driver.FindElement(_pantsDotOrg.AboutMeHeader).Displayed.Should().BeTrue();
            }
        }

        // Test 5
        [Fact]
        public void PantsSearchBarTest()
        {
            // Arrange - Navigate to pants.org 
            _pantsDotOrg.NavigateToPantsDotOrg();

            // Act - Use the page search bar to search for Pants
            _wait.Until(c => Driver.FindElement(_pantsDotOrg.SearchBar).Displayed);
            _pantsDotOrg.SearchBarSendKeys("Pants" + Keys.Return);
            _wait.Until(c => Driver.FindElement(_pantsDotOrg.SearchResultsHeader).Displayed);


            // Assert - The header and url contain the search query (Pants)
            using (new AssertionScope())
            {
                Driver.Url.Should().Contain("Pants");
                Driver.FindElement(_pantsDotOrg.SearchResultsHeader).Displayed.Should().BeTrue();
            }
        }

        // Test 6
        // Meets Requirement:
        // Negative Test
        [Fact]
        public void WordpressFailAtProvingHumanityNegativeTest()
        {
            // Arrange - Navigate to pants.org (I know I should probably just navigate directly to the
            // Wordpress login page, but pants.org is funnier)
            _pantsDotOrg.NavigateToPantsDotOrg();

            // Act - Click on Wordpress Login link and attempt to login without proving Humanity 
            _pantsDotOrg.NavigateWordPressLoginPage();
            _wordpressLoginPage.WordpressLoginFieldsSendKeysAndLogin("pants@example.com", "PantsPasowrd123", "");
            _wait.Until(c => Driver.FindElement(_wordpressLoginPage.notHumanErrorPage).Displayed);

            // Assert - Logging in without proving Humanity loads error page with expected error message
            using (new AssertionScope())
            {
                Driver.FindElement(_wordpressLoginPage.notHumanErrorPage).Displayed.Should().BeTrue();
                Driver.Title.Should().Be(_wordpressLoginPage.wordpressErrorPageTitle);
            }
        }

        // Test 7
        // Meets Requirement:
        // A date picker
        [Fact]
        public void PantsHolidaysCalendarPicker()
        {
            // Arrange - Navigate to url for May 5th 2023 - No Pants Day
            _pantsHolidaysPage.NavigateToNoPantsDayUrl();

            // Act - Select Date July 27th - Take your Pants for a Walk Day
            _pantsHolidaysPage.ChooseJuly2023();
            _pantsHolidaysPage.ClickOnJuly27th();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            _wait.Until(c => Driver.FindElement(_pantsHolidaysPage.WalkYourPantsHolidayHeader).Displayed);


            // Assert - Take your Pants for a Walk Day Url and Page elements are displayed
            using (new AssertionScope())
            {
                Driver.FindElement(_pantsHolidaysPage.WalkYourPantsHolidayHeader).Displayed.Should().BeTrue();
                Driver.Url.Should().Be(_pantsHolidaysPage.TakeYourPantsForAWalkDayUrl);
            }
        }

        // Test 8
        // Meets Requirement:
        // A file upload
        [Fact]
        public void UploadImageOfPantsTest()
        {
            // Arrange - Navigate to url for file uploader
            _fileUploaderPage.NavigateToFileUploaderUrl();
            _wait.Until(c => Driver.FindElement(_fileUploaderPage.ChooseFileButton).Displayed);


            // Act - Choose ann image and upload it
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            _fileUploaderPage.UploadPantsJacketImage(path + "/Upload/PantsJacket.jpg");

            // Assert - Image uploaded panel is present with file text & File Uploaded! header is displayed
            using (new AssertionScope())
            {
                Driver.FindElement(_fileUploaderPage.UploadedFilesPanel).Displayed.Should().BeTrue();
                Driver.FindElement(_fileUploaderPage.UploadedFilesPanel).Text.Should().Be("PantsJacket.jpg");
                Driver.FindElement(_fileUploaderPage.FileUploadedHeader).Displayed.Should().BeTrue();
            }
        }

        // Test 9
        // Meets Requirement:
        // A hover-over or tooltip
        [Fact]
        public void TestAnthillHoverOverDropdownMenu()
        {
            // Arrange - Navigate to url for Red Ants Pants
            _redAntsPantsPage.NavigateToRedAntsPantsUrl();

            // Act - Hover over ANTHILL item in nav bar
            _wait.Until(c => Driver.FindElement(_redAntsPantsPage.AnthillNavItem).Displayed);
            _actions.MoveToElement(Driver.FindElement(_redAntsPantsPage.AnthillNavItem)).Perform();
            _wait.Until(c => Driver.FindElement(_redAntsPantsPage.PantsPicsMenuItem).Displayed);

            // Assert - The contributions tooltip appears for No Pants Day (May 5th)
            Driver.FindElement(_redAntsPantsPage.PantsPicsMenuItem).Displayed.Should().BeTrue();
        }

        // Test 10
        [Fact]
        public void TestExpectedNumberOfArtCards()
        {
            // Arrange - Navigate to url for The Art Of Pants
            // Act - View The Art Of Pants Page
            _artOfPantsPages.NavigateToArtOfPantsUrl();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.PantsArtCards).Displayed);


            // Assert - There should only be 9 Featured Pants Art Cards and 6 Shop More Pants Cards
            using (new AssertionScope())
            {
                Driver.FindElements(_artOfPantsPages.PantsArtCards).Should().HaveCount(9);
                Driver.FindElements(_artOfPantsPages.ShopMorePantsCards).Should().HaveCount(6);
            }
        }

        // Test 11
        [Fact]
        public void TestAddingGreetingCardToShoppingCart()
        {
            // Arrange - Navigate to url for The Art Of Pants Greeting Cards page
            _artOfPantsPages.NavigateToArtOfPantsGreetingCardsUrl();

            // Act - Click on first greeting card on greeting cards page
            _artOfPantsPages.ChooseFirstGreetingCard();
            _artOfPantsPages.ClickAddToCartButton();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.ItemAddedToCartHeader).Displayed);

            // Assert - Item added to cart notification appears and shows confirmation header
            using (new AssertionScope())
            {
                Driver.FindElement(_artOfPantsPages.CartNotification).Displayed.Should().BeTrue();
                Driver.FindElement(_artOfPantsPages.ItemAddedToCartHeader).Text.Should().Contain("Item added to your cart");
            }
        }

        // Test 12
        [Fact]
        public void TestTopNavBarInfoDropdownOptions()
        {
            // Arrange - Navigate to url for the art of pants
            _artOfPantsPages.NavigateToArtOfPantsUrl();

            // Act - Click on the Info dropdown
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.InfoTopNavItem).Displayed);
            _artOfPantsPages.ClickInfoNavDropdown();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.InfoOptions).Displayed);
            List<string> infoOptions= new List<string>(Driver.FindElements(_artOfPantsPages.InfoOptions).Select(iw => iw.Text));

            // Assert - There are only three items that have the expected text and are in the expected order
            Driver.FindElements(_artOfPantsPages.InfoOptions).Should().HaveCount(3);
            infoOptions.Should().ContainInConsecutiveOrder("Contact", "Shipping Info", "FAQ");
        }

        // Test 13
        [Fact]
        public void TestSearchBarIcon()
        {
            // Arrange - Navigate to url for the art of pants
            _artOfPantsPages.NavigateToArtOfPantsUrl();

            // Act - Click on search icon
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.SearchIcon).Displayed);
            _artOfPantsPages.ClickSearchIcon();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.PantsFinder).Displayed);

            // Assert - The Pants Finder should be displayed
            Driver.FindElement(_artOfPantsPages.PantsFinder).Displayed.Should().BeTrue();
        }

        // Test 14
        [Fact]
        public void TestRemovingItemFromShoppingCart()
        {
            // Arrange - Add an item to the shopping cart and navigate to shopping cart
            _artOfPantsPages.NavigateToArtOfPantsGreetingCardsUrl();
            _artOfPantsPages.ChooseFirstGreetingCard();
            _artOfPantsPages.ClickAddToCartButton();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.CartNotification).Displayed);
            _artOfPantsPages.ClickViewMyCart();

            // Act - Remove Item from Shopping Cart
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.RemoveItem).Displayed);
            Driver.FindElement(_artOfPantsPages.RemoveItem).Click();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.NoPantsHeader).Displayed);

            // Assert - No Pants header is displayed and the Page Title is correct
            using (new AssertionScope())
            {
                Driver.FindElement(_artOfPantsPages.NoPantsHeader).Displayed.Should().BeTrue();
                Driver.Title.Should().Be("Your Shopping Cart – The Art of Pants");

            }
        }

        // Test 15
        [Fact]
        public void TestTopNavBar()
        {
            // Arrange - Navigate to url for pants github
            _artOfPantsPages.NavigateToArtOfPantsUrl();
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.TopNavBarItems).Displayed);

            // Act - Get list of top nav bar items
            List<string> navItems = new List<string>(Driver.FindElements(_artOfPantsPages.TopNavBarItems).Select(iw => iw.Text));
            _wait.Until(c => Driver.FindElement(_artOfPantsPages.TopNavBarItems).Displayed);

            // Assert - The contributions tooltip appears for No Pants Day (May 5th)
            using (new AssertionScope())
            {
                Driver.FindElements(_artOfPantsPages.TopNavBarItems).Should().HaveCount(4);
                navItems.Should().ContainInConsecutiveOrder("Signed Prints", "Greeting Cards", "More Pants Stuff", "Info");
            }
        }
    }
}

