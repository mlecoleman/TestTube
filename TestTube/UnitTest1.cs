using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;
using Xunit.Abstractions;


namespace TestTube

{
    public class UnitTest1 : IClassFixture<WebDriverFixture>
    {
        private readonly WebDriverFixture webDriverFixture;
        private readonly ITestOutputHelper testOutputHelper;

        public UnitTest1(WebDriverFixture webDriverFixture, ITestOutputHelper testOutputHelper)
        {
            this.webDriverFixture = webDriverFixture;
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            Console.WriteLine("First test");
            testOutputHelper.WriteLine("First test");
            webDriverFixture.ChromeDriver
                .Navigate()
                .GoToUrl("https://www.pants.org/");
        }
    }
}
