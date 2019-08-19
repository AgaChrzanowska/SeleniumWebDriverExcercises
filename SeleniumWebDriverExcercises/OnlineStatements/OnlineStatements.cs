using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebDriverExcercises.OnlineStatements
{
    [TestClass]
    public class OnlainStatements
    {
        private static IWebDriver _driver = new ChromeDriver();
        private Common _common = new Common(_driver);

        [TestInitialize]
        public void SetUp()
        {
            _common.Login();
            string url = "http://zero.webappsecurity.com/bank/online-statements.html";
            _driver.Navigate().GoToUrl(url);
        }

        [TestMethod]
        public void Check_Number_Headers_And_Tables()
        {
            Assert.AreEqual(GetNumberHeaders(), GetNumberTables());
        }
        private int GetNumberHeaders()
        {
            IReadOnlyCollection<IWebElement> headers = _driver.FindElements(By.ClassName("board-header"));
            return headers.Count;
        }
        private int GetNumberTables()
        {
            IReadOnlyCollection<IWebElement> tables = _driver.FindElements(By.ClassName("board-content"));
            return tables.Count;
        }
        [TestMethod]
        public void Check_Headers_Names()
        {
            Assert.AreEqual("Statements & Documents", GetHeaderName(0));
            Assert.AreEqual("Account - Savings", GetHeaderName(1));
        }
        private string GetHeaderName(int index)
        {
            _common.MyWait(5, By.ClassName("board-header"));
            IReadOnlyCollection<IWebElement> headers = _driver.FindElements(By.ClassName("board-header"));
            string name = headers.ElementAt(index).Text;
            return name;
        }
        [TestMethod]
        public void Check_Text_First_And_Second_Table()
        {
            IWebElement firstTableText = _driver.FindElement(By.ClassName("control-label"));
            _common.MyWait(5, By.ClassName("pull-left"));
            IWebElement secondTableText = _driver.FindElement(By.ClassName("pull-left"));
            Assert.AreEqual("Account", firstTableText.Text);
            Assert.AreEqual("Recent Statements", secondTableText.Text);
        }

        [TestCleanup]
        public void Finish()
        {
            _driver.Quit();
        }
    }
}
