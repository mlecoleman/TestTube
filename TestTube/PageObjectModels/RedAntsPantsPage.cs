using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestTube.PageObjectModels
{
	public class RedAntsPantsPage
	{
        private readonly ChromeDriver Driver;

        internal string RedAntsPantsUrl = "https://redantspants.com/";

        string antHillXpath = "//span[contains(text(), \"ANTHILL\")]";
        internal By AnthillNavItem { get => By.XPath(antHillXpath); }

        string contributionsTooltipXpath = "//ul[@role=\"menubar dropdown\"]";
        internal By ContributionsTooltip { get => By.XPath(contributionsTooltipXpath); }

        string noPantsPicsMenuItemId = "menu-item-11636";
        internal By PantsPicsMenuItem { get => By.Id(noPantsPicsMenuItemId); }

        public RedAntsPantsPage(ChromeDriver driver)
        {
            Driver = driver;
        }

        internal void NavigateToRedAntsPantsUrl()
        {
            Driver.Navigate().GoToUrl(RedAntsPantsUrl);
        }
    }
}

