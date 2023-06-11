using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing21Vek.Utilities;

namespace Testing21Vek.PageObjects
{
    public class OrderPage : BasePage
    {
        private readonly By _productName = By.XPath("//a[contains(@class, 'BasketItem')]");
        private readonly By _productPriceInteger = By.XPath("//div[@data-testid='total-price']/span[1]");
        private readonly By _productPriceFraction = By.XPath("//div[@data-testid='total-price']/span[2]");
        private readonly By _promocodeTextbox = By.XPath("//input[@name='promocode']");
        private readonly By _promocodeButton = By.XPath("//button[contains(@class, 'Promocode')]");
        private readonly By _createOrderButton = By.XPath("//div[text()='Оформить заказ']");
        private readonly By _newClientTab = By.XPath("//span[text()='Новый клиент']");
        private readonly By _nextButton = By.XPath("//div[text()='Продолжить']");
        private readonly By _emailErrorMessage = By.XPath("//input[@name='email']/../following-sibling::div/span[2]");
        private readonly By _phoneErrorMessage = By.XPath("//input[@type='tel']/../../../following-sibling::div/span[2]");
        private readonly By _promocodeErrorMessage = By.XPath("//span[text()='Промокод не действует на товары в корзине']");

        public OrderPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void VerifyProductIsPresentInBasket(string expectedProductName)
        {
            WaitUntil.WaitElement(driver, _productName);
            string actualProductName = driver.FindElement(_productName).Text;
            Assert.AreEqual(expectedProductName, actualProductName, "The needed product is not present in basket");
        }

        public void VerifyProductPrice(string expectedPrice)
        {
            WaitUntil.WaitElement(driver, _productPriceInteger);
            string actualPrice = driver.FindElement(_productPriceInteger).Text + driver.FindElement(_productPriceFraction).Text;
            actualPrice = actualPrice.Substring(0, actualPrice.LastIndexOf(" "));
            Assert.AreEqual(expectedPrice, actualPrice, "The needed product is not present in basket");
        }

        public void EnterPromocode(string promocode, out bool promocodePassed)
        {
            WaitUntil.WaitElement(driver, _promocodeTextbox);
            driver.FindElement(_promocodeTextbox).SendKeys(promocode);
            driver.FindElement(_promocodeButton).Click();
            if (IsElementPresent(_promocodeErrorMessage))
                promocodePassed = true;
            else
                promocodePassed = false;

        }

        public void VerifyPriceIsReduced(string initialPrice)
        {
            WaitUntil.WaitElement(driver, _productPriceInteger);
            Thread.Sleep(200);
            string newPrice = driver.FindElement(_productPriceInteger).Text + driver.FindElement(_productPriceFraction).Text;
            newPrice = newPrice.Substring(0, newPrice.LastIndexOf(" "));
            double initialPriceNumber = double.Parse(initialPrice);
            double newPriceNumber = double.Parse(newPrice);
            Assert.IsTrue(initialPriceNumber > newPriceNumber, "Price with promocode is not less that initial price");
        }

        public void ClickOnCreateOrderButton()
        {
            WaitUntil.WaitElement(driver, _createOrderButton);
            driver.FindElement(_createOrderButton).Click();
        }

        public void ClickOnNewClientTab()
        {
            WaitUntil.WaitElement(driver, _newClientTab);
            Thread.Sleep(500);
            driver.FindElement(_newClientTab).Click();
        }

        public void ClickOnNextButton()
        {
            WaitUntil.WaitElement(driver, _nextButton);
            driver.FindElement(_nextButton).Click();
        }

        public void VerifyEmailIsNotFilledErrorMessage(string expectedErrorMessage)
        {
            WaitUntil.WaitElement(driver, _emailErrorMessage);
            Thread.Sleep(100);
            string actualErrorMessage = driver.FindElement(_emailErrorMessage).Text;
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "Email error message is not correct");
        }

        public void VerifyPhoneNotFilledErrorMessage(string expectedErrorMessage)
        {
            WaitUntil.WaitElement(driver, _phoneErrorMessage);
            string actualErrorMessage = driver.FindElement(_phoneErrorMessage).Text;
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "Phone error message is not correct");
        }
    }
}
