using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing21Vek.Utilities;

namespace Testing21Vek.PageObjects
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected string productName;

        public BasePage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public void NavigateToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        protected bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
