using System;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TestTube
{
    public class WebDriverFixture : IDisposable
    {
        public ChromeDriver ChromeDriver { get; private set; }

        public WebDriverFixture()
        {
            var driver = new DriverManager().SetUpDriver(new ChromeConfig());
            ChromeDriver = new ChromeDriver();
        }


        public void Dispose()
        {
            ChromeDriver.Quit();
            ChromeDriver.Dispose();
        }
    }
}

