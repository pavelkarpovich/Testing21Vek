using OpenQA.Selenium;
using System.Data;
using Testing21Vek.Utilities;

namespace Testing21Vek.PageObjects
{
    public class CatalogPage : BasePage
    {
        private readonly By _minPriceTextbox = By.XPath("//dt[contains(text(), 'Цена,')]/../dd/label[1]//input");
        private readonly By _maxPriceTextbox = By.XPath("//dt[contains(text(), 'Цена,')]/../dd/label[2]//input");
        private readonly By _inPlaceCheckbox = By.XPath("//label[contains(text(), 'В наличии')]");
        private readonly By _onOrderCheckbox = By.XPath("//label[contains(text(), 'Под заказ')]");
        private readonly By _productLineShowMoreLink = By.XPath("//dt/span[text()='Линейка']/../..//span[text()='Показать всё']");
        private readonly By _productLineLink = By.XPath("//dt/span[text()='Линейка']");
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

        public void SetOnOrder()
        {
            WaitUntil.WaitElement(driver, _onOrderCheckbox);
            driver.FindElement(_onOrderCheckbox).Click();
        }

        public void SelectProductBrand(params string[] brands)
        {
            for (int i = 0; i < brands.Length; i++)
            {
                var productLineLocator = GetProductFilterItemLocator(brands[i]);
                if (IsElementPresent(productLineLocator))
                    driver.FindElement(productLineLocator).Click();
            }
        }

        public void SelectProductLines(params string[] lines)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 500)");

            WaitUntil.WaitElement(driver, _productLineLink);
            driver.FindElement(_productLineLink).Click();

            if (IsElementPresent(_productLineShowMoreLink))
                driver.FindElement(_productLineShowMoreLink).Click();

            for (int i = 0; i < lines.Length; i++)
            {
                var productLineLocator = GetProductFilterItemLocator(lines[i]);
                if (IsElementPresent(productLineLocator))
                    driver.FindElement(productLineLocator).Click();
            }
        }

        protected By GetProductFilterItemLocator(string item)
        {
            return By.XPath($"//dd/label[text()='{item}']");
        }

        public void AddToComparisonProductWithAveragePrice()
        {
            WaitUntil.WaitElement(driver, _priceText);
            var priceList = driver.FindElements(_priceText).Select(x => double.Parse(x.Text.Remove(x.Text.IndexOf(" "), 1))).ToList();
            double average = priceList.Sum() / priceList.Count;
            var diffList = priceList.Select(x => Math.Abs(x - average)).ToList();
            var index = diffList.IndexOf(diffList.Min());
            var addToComparisonLocator = GetAddToComparisonLocator(index+1);
            driver.FindElement(addToComparisonLocator).Click();
        }

        public void AddToComparisonProductWithMaxPrice()
        {
            WaitUntil.WaitElement(driver, _priceText);
            var priceList = driver.FindElements(_priceText).Select(x => double.Parse(x.Text.Remove(x.Text.IndexOf(" "), 1))).ToList();
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
            CloseBanner();
            driver.FindElement(_compareProductsLink).Click();
        }

        private By GetAddToComparisonLocator(int index)
        {
            return By.XPath($"//ul[contains(@class, 'result')]/li[{index}]//a[text()='Добавить в сравнение']");
        }
    }
}
