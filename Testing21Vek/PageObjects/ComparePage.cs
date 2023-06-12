using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Testing21Vek.Utilities;

namespace Testing21Vek.PageObjects
{
    public class ComparePage : BasePage
    {
        private readonly By _ShowOnlyDifferenceButton = By.XPath("//span[text()='Показать только отличия']");
        private readonly By _toBusketLinkButton = By.XPath($"//div[contains(@class, 'front')]/table/thead/tr[1]/td[2]//table//button[text()='В корзину']");
        private readonly By _firstProduct = By.XPath("//div[contains(@class, 'front')]/table/thead/tr[1]/td[2]/div/a[text()='Удалить из сравнения']");
        private readonly By _productName = By.XPath("//h1[@itemprop='name']");
        private readonly By _productImage = By.XPath("//div[contains(@class, 'front')]/table/thead/tr[1]/td[2]/div/a/img");
        private readonly By _productPrice = By.XPath("//div[@itemprop='offers']/span/span[@itemprop='price']");

        public ComparePage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void ShowOnlyDifference()
        {
            WaitUntil.WaitElement(driver, _ShowOnlyDifferenceButton);
            driver.FindElement(_ShowOnlyDifferenceButton).Click();
        }

        public void RemoveProductWithLessValueOfParameter(string parameter)
        {
            var valueList = GetValuesOfParameter(parameter);
            int index;
            if (valueList.Any(x => string.IsNullOrEmpty(x)))
                index = 0;
            else
            {
                List<int> valueListNumbers = valueList.Select(x => int.Parse(x.Substring(0, x.IndexOf(' ')))).ToList();
                index = valueListNumbers.IndexOf(valueListNumbers.Min());
            }

            var locator = GetRemoveProductLocator(index + 2);
            WaitUntil.WaitElement(driver, locator);
            var deleteElement = driver.FindElement(locator);
            Actions action = new Actions(driver);
            action.MoveToElement(deleteElement).Click().Perform();
        }

        public void PutProductToBusket(out string productName, out string productPrice)
        {
            WaitUntil.WaitElement(driver, _productImage);
            driver.FindElement(_productImage).Click();

            WaitUntil.WaitElement(driver, _productName);
            productName = driver.FindElement(_productName).Text;
            productPrice = driver.FindElement(_productPrice).Text;
            driver.Navigate().Back();

            WaitUntil.WaitElement(driver, _firstProduct);
            var firstProductElement = driver.FindElement(_firstProduct);
            Actions action = new Actions(driver);
            action.MoveToElement(firstProductElement).Perform();
            driver.FindElement(_toBusketLinkButton).Click();
        }

        private List<string> GetValuesOfParameter(string parameter)
        {
            var locator = By.XPath($"//div[@class='compare_container__front']//*[text()='{parameter}']/../td[contains(@class, 'value')]");
            WaitUntil.WaitElement(driver, locator);
            return driver.FindElements(locator).Select(x => x.Text).ToList();
        }

        private By GetRemoveProductLocator(int index)
        {
            return By.XPath($"//div[contains(@class, 'front')]/table/thead/tr[1]/td[{index}]/div/a[text()='Удалить из сравнения']");
        }
    }
}
