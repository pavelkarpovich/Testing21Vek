namespace Testing21Vek.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ParallelTests : BaseTest
    {
        [Test]
        public void Tabs()
        {
            mainPage.NavigateToUrl(TestData.AppUrl);
            mainPage.AcceptCookies();
            mainPage.ClickOnCatalogButton();
            mainPage.ClickOnProductGroup("Компьютеры и периферия");
            mainPage.ClickOnProductSubGroup("Планшеты");
            padsPage.SetPriceRange("500", "2000");
            padsPage.SetInPlace();
            padsPage.SelectProductBrand("Apple");
            padsPage.SelectProductLines("iPad Air (Apple)", "iPad 2021 (Apple)");
            padsPage.ClickOnShowProductsButton();
            padsPage.AddToComparisonProductWithAveragePrice();
            padsPage.AddToComparisonProductWithMaxPrice();
            padsPage.ClickOnCompareProductsLink();
            comparePage.ShowOnlyDifference();
            comparePage.RemoveProductWithLessValueOfParameter("Встроенная память");
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

        [Test]
        public void GameConsoles()
        {
            mainPage.NavigateToUrl(TestData.AppUrl);
            mainPage.AcceptCookies();
            mainPage.ClickOnCatalogButton();
            mainPage.ClickOnProductGroup("Компьютеры и периферия");
            mainPage.ClickOnProductSubGroup("Игровые приставки");
            consolesPage.SetPriceRange("1500", "3000");
            consolesPage.SetInPlace();
            consolesPage.SetOnOrder();
            consolesPage.SelectProductBrand("Microsoft", "Sony");
            consolesPage.SelectProductLines("Xbox Series X", "Sony PS5");
            consolesPage.ClickOnShowProductsButton();
            consolesPage.AddToComparisonProductWithAveragePrice();
            consolesPage.AddToComparisonProductWithMaxPrice();
            consolesPage.ClickOnCompareProductsLink();
            comparePage.ShowOnlyDifference();
            comparePage.RemoveProductWithLessValueOfParameter("Частота графического процессора");
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
