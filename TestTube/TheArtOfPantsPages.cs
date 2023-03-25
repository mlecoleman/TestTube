using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestTube
{
    public class TheArtOfPantsPages
    {
        private readonly ChromeDriver Driver;

        public TheArtOfPantsPages(ChromeDriver driver)
        {
            Driver = driver;
        }

        internal string TheArtOfPantsUrl = "https://theartofpants.com/";
        internal string TheArtOfPantsGreetingCardsUrl = "https://theartofpants.com/collections/greeting-cards";

        string searchIconXpath = "//summary[@aria-label=\"Pants finder\"]";
        internal By SearchIcon { get => By.XPath(searchIconXpath); }

        string pantsFinderName = "q";
        internal By PantsFinder { get => By.Name(pantsFinderName); }

        string pantsArtCardsXpath = "//li[@class=\"grid__item\"]";
        internal By PantsArtCards { get => By.XPath(pantsArtCardsXpath); }

        string shopMorePantsCardsXpath = "//li[@class=\"grid__item\"]";
        internal By ShopMorePantsCards { get => By.XPath(shopMorePantsCardsXpath); }

        string topNavBarItemsXpath = "//nav[@class=\"header__inline-menu\"]//child::span";
        internal By TopNavBarItems { get => By.XPath(topNavBarItemsXpath); }

        string infoTopNavItem = "//details[@id=\"Details-HeaderMenu-4\"]//span[contains(text(), \"Info\")]";
        internal By InfoTopNavItem { get => By.XPath(infoTopNavItem); }

        string infoOptionsXpath = "//ul[@id=\"HeaderMenu-MenuList-4\"]//li//a";
        internal By InfoOptions { get => By.XPath(infoOptionsXpath ); }

        string addToCartButtonXpath = "//button[@name=\"add\"]";
        internal By AddToCartButton { get => By.XPath(addToCartButtonXpath); }

        string itemAddedToCartHeaderXpath = "//div[@id=\"cart-notification\"]//child::h2";
        internal By ItemAddedToCartHeader { get => By.XPath(itemAddedToCartHeaderXpath); }

        string cartNotificationId = "cart-notification";
        internal By CartNotification { get => By.Id(cartNotificationId); }

        string viewMyCartLinkId = "cart-notification-button";
        internal By ViewMyCartLink { get => By.Id(viewMyCartLinkId); }

        string removeItemIconId = "Remove-1";
        internal By RemoveItem { get => By.Id(removeItemIconId); }

        string noPantsHeaderClass = "cart__empty-text";
        internal By NoPantsHeader { get => By.ClassName(noPantsHeaderClass); }

        internal void ChooseFirstGreetingCard()
        {
            IWebElement firstGreetingCard = Driver.FindElements(PantsArtCards).First();
            firstGreetingCard.Click();
        }
    }
}

