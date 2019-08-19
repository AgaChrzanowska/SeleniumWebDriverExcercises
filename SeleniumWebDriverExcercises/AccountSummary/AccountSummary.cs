using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebDriverExcercises.AccountSummary
{
    [TestClass]
    public class AccountSummary
    {
        private static IWebDriver _driver = new ChromeDriver();
        private Common _common = new Common(_driver);

        [TestInitialize]
        public void SetUp()
        {
            _common.Login();
            string url = "http://zero.webappsecurity.com/bank/account-summary.html";
            _driver.Navigate().GoToUrl(url);
        }
        [TestMethod]
        public void Chect_Table_Name()
        {
            IWebElement table_Name = _driver.FindElement(By.Id("account_summary_tab"));
            Assert.AreEqual("Account Summary", table_Name.Text);
        }

        [TestMethod]
        public void Check_Headers_Count()
        {
            Assert.AreEqual(4, GetHeadersCount());
        }
        private int GetHeadersCount()
        {
            IReadOnlyCollection<IWebElement> tableHeaders = _driver.FindElements(By.ClassName("board-header"));
            return tableHeaders.Count;
        }

        [TestMethod]
        public void Check_Tables_Counts()
        {
            Assert.AreEqual(4, GetTablesCount());
        }
        private int GetTablesCount()
        {
            IReadOnlyCollection<IWebElement> tableCount = _driver.FindElements(By.ClassName("board-content"));
            return tableCount.Count;
        }
        [TestMethod]
        public void Check_If_Table_Count_Equals_Headers_Count()
        {
            Assert.AreEqual(GetHeadersCount(), GetTablesCount());
        }
        [TestMethod]
        public void Check_First_Text()
        {
            Assert.AreEqual("Savings", GetTextAtFirstColumnAndFirstRowInFirstTable());
        }
        private string GetTextAtFirstColumnAndFirstRowInFirstTable()
        {
            IReadOnlyCollection<IWebElement> tableCashAcoount = _driver.FindElements(By.TagName("td"));
            return tableCashAcoount.ElementAt(0).Text;
        }

        [TestCleanup]
        public void Finish()
        {
            _driver.Quit();
        }
    }
}
