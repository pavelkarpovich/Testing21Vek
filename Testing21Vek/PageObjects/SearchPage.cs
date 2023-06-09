using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing21Vek.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace Testing21Vek.PageObjects
{
    public class SearchPage : BasePage
    {
        private readonly By nameTextBox = By.XPath("//div[contains(@class, 'ui-dialog')]//fieldset/label[contains(text(), 'Имя:')]//input");
        private readonly By emailTextBox = By.XPath("//div[contains(@class, 'ui-dialog')]//fieldset/label[contains(text(), 'E-mail:')]//input");
        private readonly By agreementCheckBox = By.XPath("//label[contains(@class, 'agreement-checkbox')]");
        private readonly By sendButton = By.XPath("//button/span[text()='Отправить']");
        private readonly By notificationMessage = By.XPath("//span[text()='Узнать о поступлении']/../following-sibling::div/div");
        private readonly By closeButton = By.XPath("//a/span[text()='Закрыть']");

        public SearchPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void ClickNotifyButtonForProduct(string productName)
        {
            By notifyButton = GetNotifyButtonLocatorForProduct(productName);
            WaitUntil.WaitElement(driver, notifyButton);
            driver.FindElement(notifyButton).Click();
        }

        public void SendUserDataForNotification(string name, string email)
        {
            WaitUntil.WaitElement(driver, nameTextBox);
            driver.FindElement(nameTextBox).SendKeys(name);
            driver.FindElement(emailTextBox).SendKeys(email);
            driver.FindElement(agreementCheckBox).Click();
            driver.FindElement(sendButton).Click();
        }

        public void VerifyNotificationText(string expectedText)
        {
            WaitUntil.WaitElement(driver, notificationMessage);
            string actualText = driver.FindElement(notificationMessage).Text;
            Assert.AreEqual(expectedText, actualText, "Notification text is not correct");
        }

        public void ClosePopup()
        {
            driver.FindElement(closeButton).Click();
        }

        public void VerifyAwaitingMessageForProduct(string productName)
        {
            bool isAwaitingMessage = IsElementPresent(GetAwaitingMessageLocatorForProduct(productName));
        }

        private By GetNotifyButtonLocatorForProduct(string productName)
        {
            return By.XPath($"//span[text()='{productName}']/ancestor::dl//a[text()='Узнать о поступлении']");
        }

        private By GetAwaitingMessageLocatorForProduct(string productName)
        {
            return By.XPath($"//span[text()='{productName}']/ancestor::dl//span[text()='В листе ожидания']");
        }
    }
}
