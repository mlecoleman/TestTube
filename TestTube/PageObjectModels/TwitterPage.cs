using System;
using OpenQA.Selenium;

namespace TestTube.PageObjectModels
{
    internal class TwitterPage
    {
        internal string twitterUrl = "https://twitter.com/PalmerCCIE";

        internal string twitterTitle = "Palmer Sample (@PalmerCCIE) / Twitter";

        string gitTwitterUserUrlLinkXpath = "//*[@data-testid=\"UserUrl\"]//child::span[contains(text(), 'pants.org')]";
        internal By gitTwitterUserUrlLink { get => By.XPath(gitTwitterUserUrlLinkXpath); }
    }
}

