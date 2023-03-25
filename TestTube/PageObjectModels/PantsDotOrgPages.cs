using System;
using OpenQA.Selenium;

namespace TestTube.PageObjectModels
{
	internal class PantsDotOrgPages
	{
		public string pantsDotOrgUrl = "https://www.pants.org/";
        public string pantsAboutMeUrl = "https://www.pants.org/about-me/";

        string twitterButtonXpath = "//*[@id=\"social\"]//child::a[@title=\"pants.org Twitter\"]";
		internal By TwitterButton { get => By.XPath(twitterButtonXpath); }

        string wordpressLoginLinkXpath = "//*[@id=\"footer-widget-container\"]//child::a[contains(text(), 'Log in')]";
        internal By wordpressLoginLink { get => By.XPath(wordpressLoginLinkXpath); }

        string navBarAboutMeXpath = "//ul[@id=\"menu-menu\"]//a[contains(text(), \"About Me\")]";
        internal By NavBarAboutMe { get => By.XPath(navBarAboutMeXpath); }

        string aboutMeHeaderXpath = "//h1[contains(text(), \"About Me\")]";
        internal By AboutMeHeader { get => By.XPath(aboutMeHeaderXpath); }

        string searchBarXpath = "//form[@id=\"searchform\"]//input";
        internal By SearchBar { get => By.XPath(searchBarXpath); }

        string searchResultsHeaderXpath = "//form[@id=\"searchform\"]//input";
        internal By SearchResultsHeader { get => By.XPath(searchResultsHeaderXpath); }

        string paginationNumbersXpath = "//a[@class=\"page-numbers\"]";
        internal By paginationNumbers { get => By.XPath(paginationNumbersXpath); }

        string paginationCurrentPageClass = "active";
        internal By PaginationCurrentPage { get => By.ClassName(paginationCurrentPageClass); }


    }
}

