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

namespace TestTube

{
    public class UnitTest1 : IClassFixture<WebDriverFixture>, IDisposable
    {
        private readonly WebDriverFixture webDriverFixture;
        private readonly ITestOutputHelper testOutputHelper;
        ChromeDriver _driver;
        PantsDotOrg _pantsDotOrg;
        TwitterPage _twitterPage;

        public UnitTest1(WebDriverFixture webDriverFixture, ITestOutputHelper testOutputHelper)
        {
            this.webDriverFixture = webDriverFixture;
            this.testOutputHelper = testOutputHelper;
            var driver = webDriverFixture.ChromeDriver;
            _driver = driver;
            PantsDotOrg pantsDotOrg = new PantsDotOrg();
            _pantsDotOrg = pantsDotOrg;
            TwitterPage twitterPage = new TwitterPage();
            _twitterPage = twitterPage;
        }

        [Fact]
        public void NewTab()
        {
            // Arrange
            //var driver = webDriverFixture.ChromeDriver;
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            _driver
                .Navigate()
                .GoToUrl(_pantsDotOrg.pantsDotOrgUrl);

            // Act 
            //string twitterXPath = "//*[@id=\"social\"]//child::a[@title=\"pants.org Twitter\"]";
            var twitterButton = _driver.FindElement(_pantsDotOrg.gitTwitterButton);
            twitterButton.Click();

            // Assert
            //string twitterUrl = "https://twitter.com/PalmerCCIE";
            //string twitterTitle = "Palmer Sample (@PalmerCCIE) / Twitter";
            //string twitterUserUrlLink = "//*[@data-testid=\"UserUrl\"]//child::span[contains(text(), 'pants.org')]";
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            IWebElement twitterTitleLoaded =
                wait.Until((d) => d.FindElement(_twitterPage.gitTwitterUserUrlLink));

            using (new AssertionScope())
            {
                _driver.WindowHandles.Should().HaveCount(2);
                _driver.Url.Should().Be(_twitterPage.twitterUrl);
                _driver.Title.Should().Be(_twitterPage.twitterTitle);
            } 
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}


