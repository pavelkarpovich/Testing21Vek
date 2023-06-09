using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing21Vek.PageObjects;

namespace Testing21Vek.Tests
{
    [TestFixture]
    public class BaseTest
    {
        protected IWebDriver driver;
        protected MainPage mainPage;
        protected SearchPage searchPage;
        protected NotebooksPage notebooksPage;
        protected NotebooksComparePage notebooksComparePage;
        protected OrderPage orderPage;

        [SetUp]
        public void Setup()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--incognito");
            driver = new ChromeDriver("/Drivers/chromedriver.exe", chromeOptions);

            mainPage = new MainPage(driver);
            searchPage = new SearchPage(driver);
            notebooksPage = new NotebooksPage(driver);
            notebooksComparePage = new NotebooksComparePage(driver);
            orderPage = new OrderPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
