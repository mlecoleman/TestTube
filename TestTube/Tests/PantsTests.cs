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

namespace TestTube

{
    public sealed class PantsTests : BaseTest
    {
        private readonly ITestOutputHelper testOutputHelper;
        PantsDotOrg _pantsDotOrg;
        TwitterPage _twitterPage;
        WordpressLoginPage _wordpressLoginPage;
        PantsHolidaysPage _pantsHolidaysPage;
        FileUploaderPage _fileUploaderPage;
        

        public PantsTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;

            PantsDotOrg pantsDotOrg = new PantsDotOrg();
            _pantsDotOrg = pantsDotOrg;

            TwitterPage twitterPage = new TwitterPage();
            _twitterPage = twitterPage;

            WordpressLoginPage wordpressLoginPage = new WordpressLoginPage();
            _wordpressLoginPage = wordpressLoginPage;

            PantsHolidaysPage pantsHolidaysPage = new PantsHolidaysPage(Driver);
            _pantsHolidaysPage = pantsHolidaysPage;

            FileUploaderPage fileUploaderPage = new FileUploaderPage(Driver);
            _fileUploaderPage = fileUploaderPage;

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
            Driver
                .Navigate()
                .GoToUrl(_pantsDotOrg.pantsDotOrgUrl);

            // Act - Click on the Twitter Button/Link and wait for the new tab to be fully loaded
            Driver.FindElement(_pantsDotOrg.gitTwitterButton).Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            wait.Until(c => Driver.FindElement(_twitterPage.gitTwitterUserUrlLink).Displayed);

            // Assert - 2 Windows are open and the new window has the expected Twitter title & URL
            using (new AssertionScope())
            {
                Driver.WindowHandles.Should().HaveCount(2);
                Driver.Url.Should().Be(_twitterPage.twitterUrl);
                Driver.Title.Should().Be(_twitterPage.twitterTitle);
            }
        }

        // Test 2
        // Meets Requirement:
        // Negative test.
        [Fact(Skip = "Not Finished")]
        public void WordpressInvalidLoginNegativeTest()
        {
            // Arrange - Navigate to pants.org (I know I should probably just navigate directly to the
            // Wordpress login page, but pants.org is funnier)
            Driver.Navigate().GoToUrl(_pantsDotOrg.pantsDotOrgUrl);

            // Act - Click on Wordpress Login link and attempt to login with invalid credentials 
            Driver.FindElement(_pantsDotOrg.wordpressLoginLink).Click();
            Driver.FindElement(_wordpressLoginPage.emailAddressInput).SendKeys("pants@example.com");
            Driver.FindElement(_wordpressLoginPage.emailAddressInput).SendKeys("pantsdotorgpassword");
            Driver.FindElement(_wordpressLoginPage.proveHumanityInput).SendKeys("");
            Driver.FindElement(_wordpressLoginPage.loginButton).Click();

            // Assert - Logging in with invalid credentials triggers login error message
            using (new AssertionScope())
            {
                Driver.FindElement(_wordpressLoginPage.invalidLoginCredentialsError).Displayed.Should().BeTrue();
            }
        }

        // Test 3
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
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(c => Driver.FindElement(_wordpressLoginPage.notHumanErrorPage).Displayed);

            // Assert - Logging in without proving Humanity loads error page with expected error message
            using (new AssertionScope())
            {
                Driver.FindElement(_wordpressLoginPage.notHumanErrorPage).Displayed.Should().BeTrue();
                Driver.Title.Should().Be(_wordpressLoginPage.wordpressErrorPageTitle);
            }
        }
    }
}


//Create a suite of automated UI tests for publicly accessible web applications. You may use any
//online web application you choose, and you are not required to write all of your tests against the
//same application. You may write your tests in the same project as your API tests from the
//midterm, or you may create a new one.You may use any Nuget packages, test frameworks, or
//assertion libraries you choose, but your solution must be in C#. Create at least 15 automated UI
//tests. Your tests should each be unique and test a different use case of the web application.
//Your tests should also cover all of the following criteria:
//1.Each test must contain an assertion.
//2. 
//3. Include at least one test for each of the following complex page controls:
//a.A file upload
//b. A date picker
//c. A hover-over or tooltip
//4. 
//5.Utilize a Page Object Model. Create at least 3 Page Object Model classes that represent
//web pages under test.
//Ensure that at the end of your tests, any and all open browser windows or sessions are closed
//and disposed of properly.
//Build your code and run your tests. All tests should run and all assertions should pass in order
//to receive credit. If your code requires any setup in order to build or run your tests, include a
//readme file with instructions to build and run your code. Check your completed tests into a
//GitHub repository, and submit that link as your turn-in.

