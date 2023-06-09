using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing21Vek.Utilities;

namespace Testing21Vek.PageObjects
{
    public class NotebooksPage : CatalogPage
    {
        private readonly By _productLineLink = By.XPath("//dt/span[text()='Линейка']");
        private readonly By _productTypeLink = By.XPath("//dt/span[text()='Тип']");
        private readonly By _productLineShowMoreLink = By.XPath("//dt/span[text()='Линейка']/../..//span[text()='Показать всё']");

        public NotebooksPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void SelectProductLines(params string[] lines)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 500)");

            WaitUntil.WaitElement(driver, _productLineLink);
            driver.FindElement(_productLineLink).Click();
            WaitUntil.WaitElement(driver, _productLineShowMoreLink);

            driver.FindElement(_productLineShowMoreLink).Click();

            for (int i = 0; i < lines.Length; i++)
            {
                var productLineLocator = GetProductFilterItemLocator(lines[i]);
                driver.FindElement(productLineLocator).Click();
            }
        }

        public void SelectProductType(string type)  //ToDo: use params
        {
            WaitUntil.WaitElement(driver, _productTypeLink);
            driver.FindElement(_productTypeLink).Click();

            var productTypeLocator = GetProductFilterItemLocator(type);
            WaitUntil.WaitElement(driver, productTypeLocator);
            driver.FindElement(productTypeLocator).Click();
        }

        private By GetProductFilterItemLocator(string item)
        {
            return By.XPath($"//dd/label[text()='{item}']");
        }
    }
}
