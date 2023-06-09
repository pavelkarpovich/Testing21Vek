using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing21Vek.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace Testing21Vek.PageObjects
{
    public class CatalogPage : BasePage
    {
        private readonly By _minPriceTextbox = By.XPath("//dt[contains(text(), 'Цена,')]/../dd/label[1]//input");
        private readonly By _maxPriceTextbox = By.XPath("//dt[contains(text(), 'Цена,')]/../dd/label[2]//input");
        private readonly By _inPlaceCheckbox = By.XPath("//label[contains(text(), 'В наличии')]");
        private readonly By _showProductsButton = By.XPath("//span[contains(text(), 'Показать товары')]");
        private readonly By _priceText = By.XPath("//ul[contains(@class, 'result')]/li//span[contains(@class, 'item-data')]");
        private readonly By _compareProductsLink = By.LinkText("Сравнить товары");
        private readonly By _banner = By.ClassName("popmechanic-close");

        public CatalogPage(IWebDriver webDriver) : base (webDriver)
        {
        }

        public void CloseBanner()
        {
            if (IsElementPresent(_banner))
            {
                driver.FindElement(_banner).Click();
            }
        }

        public void SetPriceRange(string minPrice, string maxPrice)
        {
            WaitUntil.WaitElement(driver, _minPriceTextbox);
            driver.FindElement(_minPriceTextbox).SendKeys(minPrice);
            driver.FindElement(_maxPriceTextbox).SendKeys(maxPrice);
        }

        public void SetInPlace()
        {
            WaitUntil.WaitElement(driver, _inPlaceCheckbox);
            driver.FindElement(_inPlaceCheckbox).Click();
        }

        public void SelectProductBrand(string brand)  //ToDo: use params
        {
            var productBrandLocator = GetProductBrandLocator(brand);
            WaitUntil.WaitElement(driver, productBrandLocator);
            driver.FindElement(productBrandLocator).Click();
        }

        public void AddToComparisonProductWithAveragePrice()
        {
            WaitUntil.WaitElement(driver, _priceText);
            var priceList = driver.FindElements(_priceText).Select(x => Convert.ToDouble(x.Text)).ToList();
            double average = priceList.Sum() / priceList.Count;
            var diffList = priceList.Select(x => Math.Abs(x - average)).ToList();
            var index = diffList.IndexOf(diffList.Min());
            var addToComparisonLocator = GetAddToComparisonLocator(index+1);
            driver.FindElement(addToComparisonLocator).Click();
        }

        public void AddToComparisonProductWithMaxPrice()
        {
            WaitUntil.WaitElement(driver, _priceText);
            var priceList = driver.FindElements(_priceText).Select(x => Convert.ToDouble(x.Text)).ToList();
            double maxPrice = priceList.Max();
            var index = priceList.LastIndexOf(maxPrice);
            var addToComparisonLocator = GetAddToComparisonLocator(index+1);
            driver.FindElement(addToComparisonLocator).Click();
        }

        public void ClickOnShowProductsButton()
        {
            WaitUntil.WaitElement(driver, _showProductsButton);
            driver.FindElement(_showProductsButton).Click();
        }

        public void ClickOnCompareProductsLink()
        {
            WaitUntil.WaitElement(driver, _compareProductsLink);
            Thread.Sleep(100);
            CloseBanner();
            Thread.Sleep(100);
            driver.FindElement(_compareProductsLink).Click();
        }

        private By GetProductBrandLocator(string brand)
        {
            return By.XPath($"//dt[text() = 'Производители']/..//a[text() = '{brand}']");
        }

        private By GetAddToComparisonLocator(int index)
        {
            return By.XPath($"//ul[contains(@class, 'result')]/li[{index}]//a[text()='Добавить в сравнение']");
        }
    }
}
