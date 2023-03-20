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

        //string followButtonXPath = "//*[@data-testid=\"placementTracking\"]//child::span[contains(text(), 'Follow')]";
        //internal By followButton { get => By.XPath(followButtonXPath); }

        //string signUpToFollowTextXpath = "//*[@data-testid=\"sheetDialog\"]//child::span[contains(text(), 'Sign up')]";
        //internal By signUpInstructionText { get => By.XPath(signUpToFollowTextXpath); }

        //string signUpButtonXpath = "//*[@data-testid=\"sheetDialog\"]//child::span[contains(text(), 'Sign up')]";
        //internal By signUpButton { get => By.XPath(signUpToFollowTextXpath); }
    }
}

