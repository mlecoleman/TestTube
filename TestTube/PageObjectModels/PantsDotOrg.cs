using System;
using OpenQA.Selenium;

namespace TestTube.PageObjectModels
{
	internal class PantsDotOrg
	{
		public string pantsDotOrgUrl = "https://www.pants.org/";

        string gitTwitterButtonXpath = "//*[@id=\"social\"]//child::a[@title=\"pants.org Twitter\"]";
		internal By gitTwitterButton { get => By.XPath(gitTwitterButtonXpath); }
    }
}

