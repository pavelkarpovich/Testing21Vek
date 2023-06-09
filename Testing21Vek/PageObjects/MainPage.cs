using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing21Vek.Utilities;

namespace Testing21Vek.PageObjects
{
    public class MainPage : BasePage
    {
        private readonly By _acceptCookiesButton = By.XPath("//button/div[contains(text(), 'Принять')]");
        private readonly By _searchTextBox = By.Id("catalogSearch");
        private readonly By _searchButton = By.XPath("//button[contains(@class,'Search')]");
        private readonly By _catalogButton = By.XPath("//button/span[text()='Каталог товаров']");
        private readonly By _busketButton = By.XPath("//span[text()='Корзина']");

        public MainPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void AcceptCookies()
        {
            if (IsElementPresent(_acceptCookiesButton))
            {
                driver.FindElement(_acceptCookiesButton).Click();
            }
        }

        public void SearchFor(string text)
        {
            WaitUntil.WaitElement(driver, _searchTextBox);
            driver.FindElement(_searchTextBox).SendKeys(text);
            driver.FindElement(_searchButton).Click();
        }

        public void ClickOnCatalogButton()
        {
            WaitUntil.WaitElement(driver, _catalogButton);
            driver.FindElement(_catalogButton).Click();
        }

        public void ClickOnProductGroup(string group)
        {
            var productGroupLocator = GetProductGroupLocator(group);
            WaitUntil.WaitElement(driver, productGroupLocator);
            driver.FindElement(productGroupLocator).Click();
        }

        public void ClickOnProductSubGroup(string subGroup)
        {
            var productSubGroupLocator = GetProductSubGroupLocator(subGroup);
            WaitUntil.WaitElement(driver, productSubGroupLocator);
            driver.FindElement(productSubGroupLocator).Click();
        }

        public void ClickOnBusketButton()
        {
            WaitUntil.WaitElement(driver, _busketButton);
            driver.FindElement(_busketButton).Click();
        }

        private By GetProductGroupLocator(string group)
        {
            return By.XPath($"//a/span[text()='{group}']");
        }

        private By GetProductSubGroupLocator(string subGroup)
        {
            return By.XPath($"//dt/a[text()='{subGroup}']");
        }
    }
}
