using NUnit.Framework;
using System.Net.Mail;
using Testing21Vek.PageObjects;
using Testing21Vek.Utilities;

namespace Testing21Vek.Tests
{
    [TestFixture]
    public class CatalogTests : BaseTest
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
            mainPage.ClickOnProductGroup("���������� � ���������");
            mainPage.ClickOnProductSubGroup("��������");
            notebooksPage.SetPriceRange("2000", "6840");
            notebooksPage.SetInPlace();
            notebooksPage.SelectProductBrand("Lenovo");
            notebooksPage.SelectProductLines("IdeaPad L (Lenovo)", "Legion 5 Pro (Lenovo)", "ThinkPad X (Lenovo)");
            notebooksPage.SelectProductType("������� (����������)");
            notebooksPage.ClickOnShowProductsButton();
            notebooksPage.AddToComparisonProductWithAveragePrice();
            notebooksPage.AddToComparisonProductWithMaxPrice();
            notebooksPage.ClickOnCompareProductsLink();
            notebooksComparePage.ShowOnlyDifference();
            notebooksComparePage.RemoveNotebookWithLessValueOfParameter("������� SSD");
            notebooksComparePage.PutProductToBusket(out string productName, out string productPrice);
            mainPage.ClickOnBusketButton();
            orderPage.VerifyProductIsPresentInBasket(productName);
            orderPage.VerifyProductPrice(productPrice);
            orderPage.EnterPromocode(TestData.PromoCode);
            orderPage.VerifyPriceIsReduced(productPrice);
            orderPage.ClickOnCreateOrderButton();
            orderPage.ClickOnNewClientTab();
            orderPage.ClickOnNextButton();
            orderPage.VerifyEmailIsNotFilledErrorMessage(TestData.EmailErrorMessage);
            orderPage.VerifyPhoneNotFilledErrorMessage(TestData.PhoneErrorMessage);
            Thread.Sleep(5000);
        }
    }
}