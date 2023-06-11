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
        private readonly By _productTypeLink = By.XPath("//dt/span[text()='Тип']");

        public NotebooksPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void SelectProductType(string type)  //ToDo: use params
        {
            WaitUntil.WaitElement(driver, _productTypeLink);
            driver.FindElement(_productTypeLink).Click();

            var productTypeLocator = GetProductFilterItemLocator(type);
            WaitUntil.WaitElement(driver, productTypeLocator);
            driver.FindElement(productTypeLocator).Click();
        }
    }
}
