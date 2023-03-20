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

namespace TestTube.Tests
{
	public class BaseTest : IDisposable
    {
        protected readonly ChromeDriver Driver;

        public BaseTest()
		{
            var driver = new DriverManager().SetUpDriver(new ChromeConfig());
            Driver = new ChromeDriver();
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        }

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}

