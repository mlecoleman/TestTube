using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestTube.PageObjectModels
{
    internal class TwitterPage
    {
        private readonly ChromeDriver Driver;

        public TwitterPage(ChromeDriver driver)
        {
            Driver = driver;
        }

        internal string twitterUrl = "https://twitter.com/PalmerCCIE";

        internal string twitterTitle = "Palmer Sample (@PalmerCCIE) / Twitter";

        string twitterUserUrlLinkXpath = "//*[@data-testid=\"UserUrl\"]//child::span[contains(text(), 'pants.org')]";
        internal By TwitterUserUrlLink { get => By.XPath(twitterUserUrlLinkXpath); }
    }
}

