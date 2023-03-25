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


namespace TestTube.PageObjectModels
{
    internal class PantsHolidaysPages
    {
        private readonly ChromeDriver Driver;

        internal string noPantsDayUrl = "https://www.timeanddate.com/holidays/fun/no-pants-day";

        internal string takeYourPantsForAWalkDayUrl = "https://www.timeanddate.com/holidays/fun/take-your-pants-for-a-walk-day";

        string walkYourPantsHeaderXpath = "//h1[contains(text(), \"Take your Pants for a Walk Day\")]";
        internal By walkYourPantsHolidayHeader { get => By.XPath(walkYourPantsHeaderXpath); }

        string monthYearDropdownId = "month";
        internal By monthYearDropdown { get => By.Id(monthYearDropdownId); }

        string july27thXpath = "//a[@href='/holidays/fun/take-your-pants-for-a-walk-day']";
        internal By july27th { get => By.XPath(july27thXpath); }

        public PantsHolidaysPages(ChromeDriver driver)
        {
            Driver = driver;
        }

        internal void chooseJuly2023()
        {
            IWebElement monthYearDropdown = Driver.FindElement(By.Id("month"));
            SelectElement monthYear = new SelectElement(monthYearDropdown);
            monthYear.SelectByText("July 2023");
        }

        internal void NavigateToNoPantsDayUrl()
        {
            Driver.Navigate().GoToUrl(noPantsDayUrl);
        }

        internal void CLickOnJuly27th()
        {
            Driver.FindElement(july27th).Click();
        }
    }
}

