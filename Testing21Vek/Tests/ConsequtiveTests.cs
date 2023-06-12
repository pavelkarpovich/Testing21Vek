using Testing21Vek.Utilities;

namespace Testing21Vek.Tests
{
    [TestFixture]
    public class ConsequtiveTests : BaseTest
    {
        [Test]
        public void Test1()
        {
            mainPage.NavigateToUrl(TestData.AppUrl);
            mainPage.AcceptCookies();
            mainPage.SearchFor(TestData.LampNational);
            searchPage.ClickNotifyButtonForProduct(TestData.LampNationalBronz);
            searchPage.SendUserDataForNotification(TestData.TestName, RandomGenerator.GetEmail());
            searchPage.VerifyNotificationText(TestData.NotificationText);
            searchPage.ClosePopup();
            searchPage.VerifyAwaitingMessageForProduct(TestData.LampNationalBronz);
        }

        [Test]
        public void Test2()
        {
            mainPage.NavigateToUrl(TestData.AppUrl);
            mainPage.AcceptCookies();
            mainPage.ClickOnCatalogButton();
            mainPage.ClickOnProductGroup("Компьютеры и периферия");
            mainPage.ClickOnProductSubGroup("Ноутбуки");
            notebooksPage.SetPriceRange("2000", "6840");
            notebooksPage.SetInPlace();
            notebooksPage.SelectProductBrand("Lenovo");
            notebooksPage.SelectProductLines("IdeaPad L (Lenovo)", "Legion 5 Pro (Lenovo)", "ThinkPad X (Lenovo)");
            notebooksPage.SelectProductType("игровой (геймерский)");
            notebooksPage.ClickOnShowProductsButton();
            notebooksPage.AddToComparisonProductWithAveragePrice();
            notebooksPage.AddToComparisonProductWithMaxPrice();
            notebooksPage.ClickOnCompareProductsLink();
            comparePage.ShowOnlyDifference();
            comparePage.RemoveProductWithLessValueOfParameter("Емкость SSD");
            comparePage.PutProductToBusket(out string productName, out string productPrice);
            mainPage.ClickOnBusketButton();
            orderPage.VerifyProductIsPresentInBasket(productName);
            orderPage.VerifyProductPrice(productPrice);
            orderPage.EnterPromocode(TestData.PromoCode, out bool promocodePassed);
            if (promocodePassed)
                orderPage.VerifyPriceIsReduced(productPrice);
            orderPage.ClickOnCreateOrderButton();
            orderPage.ClickOnNewClientTab();
            orderPage.ClickOnNextButton();
            orderPage.VerifyEmailIsNotFilledErrorMessage(TestData.EmailErrorMessage);
            orderPage.VerifyPhoneNotFilledErrorMessage(TestData.PhoneErrorMessage);
        }
    }
}