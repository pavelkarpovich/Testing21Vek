﻿using NUnit.Framework.Interfaces;
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
    public class BaseTest
    {
        [ThreadStatic]
        private static IWebDriver driver;
        protected MainPage mainPage;
        protected SearchPage searchPage;
        protected NotebooksPage notebooksPage;
        protected ComparePage сomparePage;
        protected OrderPage orderPage;
        protected PadsPage padsPage;
        protected ConsolesPage consolesPage;

        [SetUp]
        public void Setup()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--incognito");
            chromeOptions.AddArgument("--no-sandbox");
            driver = new ChromeDriver("/Drivers/chromedriver.exe", chromeOptions, TimeSpan.FromSeconds(130));

            mainPage = new MainPage(driver);
            searchPage = new SearchPage(driver);
            notebooksPage = new NotebooksPage(driver);
            сomparePage = new ComparePage(driver);
            orderPage = new OrderPage(driver);
            padsPage = new PadsPage(driver);
            consolesPage = new ConsolesPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
