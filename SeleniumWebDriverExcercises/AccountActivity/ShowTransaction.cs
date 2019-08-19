using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebDriverExcercises.AccountActivity
{
    [TestClass]
    public class ShowTransaction
    {
        private static IWebDriver _driver = new ChromeDriver();
        private Common _common = new Common(_driver);
        [TestInitialize]
        public void SetUp()
        {
            _common.Login();
        }

        [TestMethod]
        public void Test_Show_Transactions_Savings()
        {
            string url = "http://zero.webappsecurity.com/bank/account-activity.html";
            _driver.Navigate().GoToUrl(url);

            IWebElement headerHandler = _driver.FindElement(By.ClassName("board-header"));
            Assert.AreEqual("Show Transactions", headerHandler.Text);

            IWebElement descriptionHandler = _driver.FindElement(By.ClassName("help-block"));
            Assert.AreEqual("Choose an account to view.", descriptionHandler.Text);

            var accountDropdownHandler = _driver.FindElement(By.Name("accountId"));
            accountDropdownHandler.Click();
            IReadOnlyCollection<IWebElement> options = accountDropdownHandler.FindElements(By.TagName("option"));
            Assert.AreEqual(6, options.Count);

            IReadOnlyCollection<IWebElement> savingsTableRows = _driver.FindElements(By.CssSelector("#all_transactions_for_account > table > tbody > tr"));
            Assert.AreEqual(3, savingsTableRows.Count);

            IWebElement secondRow = savingsTableRows.ElementAt(1);
            IReadOnlyCollection<IWebElement> secondRowColumns = secondRow.FindElements(By.TagName("td"));
            IWebElement secondColumn = secondRowColumns.ElementAt(1);
            Assert.AreEqual("OFFICE SUPPLY", secondColumn.Text);
        }

        [TestMethod]
        public void Test_Show_Transactions_Checking()
        {
            string url = "http://zero.webappsecurity.com/bank/account-activity.html";
            _driver.Navigate().GoToUrl(url);

            IWebElement checkingButton = _driver.FindElement(By.CssSelector("#aa_accountId > option:nth-child(2)"));
            checkingButton.Click();

            _common.MyWait(6, By.CssSelector("#all_transactions_for_account > table"));

            IReadOnlyCollection<IWebElement> checkingTableRows = _driver.FindElements(By.CssSelector("#all_transactions_for_account > table > tbody > tr"));

            Assert.AreEqual(3, checkingTableRows.Count);

            IWebElement thirdRow = checkingTableRows.ElementAt(2);
            IReadOnlyCollection<IWebElement> thirdRowColumns = thirdRow.FindElements(By.TagName("td"));
            IWebElement secondColumnChecking = thirdRowColumns.ElementAt(1);
            Assert.AreEqual("CAR PAYMENT", secondColumnChecking.Text);

            IWebElement firstRow = checkingTableRows.ElementAt(0);
            IReadOnlyCollection<IWebElement> firstRowColumns = firstRow.FindElements(By.TagName("td"));
            IWebElement thirdColumnChecking = firstRowColumns.ElementAt(2);
            Assert.AreEqual("186.7", thirdColumnChecking.Text);

            IWebElement secondRow = checkingTableRows.ElementAt(1);
            IReadOnlyCollection<IWebElement> secondRowColumns = secondRow.FindElements(By.TagName("td"));
            IWebElement thirdColumnChecking2 = secondRowColumns.ElementAt(2);
            Assert.AreEqual("", thirdColumnChecking2.Text);
        }

        [TestCleanup]
        public void Finish()
        {
            _driver.Quit();
        }
    }
}
