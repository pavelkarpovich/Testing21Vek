using OpenQA.Selenium;

namespace Testing21Vek.PageObjects
{
    public class BasePage
    {
        [ThreadStatic]
        protected static IWebDriver driver;
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
