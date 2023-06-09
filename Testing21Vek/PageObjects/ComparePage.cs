using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing21Vek.Utilities;

namespace Testing21Vek.PageObjects
{
    public class ComparePage : BasePage
    {
        private readonly By _ShowOnlyDifferenceButton = By.XPath("//span[text()='Показать только отличия']");

        public ComparePage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void ShowOnlyDifference()
        {
            WaitUntil.WaitElement(driver, _ShowOnlyDifferenceButton);
            driver.FindElement(_ShowOnlyDifferenceButton).Click();
        }
    }
}
