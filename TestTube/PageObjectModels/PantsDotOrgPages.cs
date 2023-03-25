using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestTube.PageObjectModels
{
	internal class PantsDotOrgPages
	{
        private readonly ChromeDriver Driver;

        public string pantsDotOrgUrl = "https://www.pants.org/";
        public string pantsAboutMeUrl = "https://www.pants.org/about-me/";

        string siteTitleXpath = "//h2[@class=\"site-title\"]/a";
        internal By SiteTitle { get => By.XPath(siteTitleXpath); }

        string twitterButtonXpath = "//*[@id=\"social\"]//child::a[@title=\"pants.org Twitter\"]";
		internal By TwitterButton { get => By.XPath(twitterButtonXpath); }

        string wordpressLoginLinkXpath = "//*[@id=\"footer-widget-container\"]//child::a[contains(text(), 'Log in')]";
        internal By WordpressLoginLink { get => By.XPath(wordpressLoginLinkXpath); }

        string navBarAboutMeXpath = "//ul[@id=\"menu-menu\"]//a[contains(text(), \"About Me\")]";
        internal By NavBarAboutMe { get => By.XPath(navBarAboutMeXpath); }

        string aboutMeHeaderXpath = "//h1[contains(text(), \"About Me\")]";
        internal By AboutMeHeader { get => By.XPath(aboutMeHeaderXpath); }

        string searchBarXpath = "//form[@id=\"searchform\"]//input";
        internal By SearchBar { get => By.XPath(searchBarXpath); }

        string searchResultsHeaderXpath = "//form[@id=\"searchform\"]//input";
        internal By SearchResultsHeader { get => By.XPath(searchResultsHeaderXpath); }

        string paginationNumbersXpath = "//a[@class=\"page-numbers\"]";
        internal By PaginationNumbers { get => By.XPath(paginationNumbersXpath); }

        string paginationCurrentPageClass = "active";
        internal By PaginationCurrentPage { get => By.ClassName(paginationCurrentPageClass); }


        public PantsDotOrgPages(ChromeDriver driver)
        {
            Driver = driver;
        }

        internal void NavigateToPantsDotOrg()
        {
            Driver.Navigate().GoToUrl(pantsDotOrgUrl);
        }

        internal void ClickOnTwitterButton()
        {
            Driver.FindElement(TwitterButton).Click();
        }

        internal void ClickOnPage3InPagination()
        {
            Driver.FindElements(PaginationNumbers)[1].Click();
        }

        internal void CurrentPageInPagination()
        {
            Driver.FindElement(PaginationCurrentPage);
        }

        internal void ClickOnPageTitle()
        {
            Driver.FindElement(SiteTitle).Click();

        }

        internal void SearchBarSendKeys(string keys)
        {
            Driver.FindElement(SearchBar).SendKeys(keys);
        }

        internal void NavigateWordPressLoginPage()
        {
            Driver.Navigate().GoToUrl(pantsDotOrgUrl);
            Driver.FindElement(WordpressLoginLink).Click();
        }
    }
}

