using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebDriverExcercises.PayBills
{
    [TestClass]
    public class CommonForPayBills
    {
        private static IWebDriver _driver = new ChromeDriver();
        private Common _common = new Common(_driver);
        

        [TestInitialize]
        public void SetUp()
        {
            _common.Login();
            IWebElement btnPayBills = _driver.FindElement(By.Id("pay_bills_tab"));
            btnPayBills.Click();
        }

        [TestMethod]
        public void Check_Header_Name()
        {
            IWebElement headerName = _driver.FindElement(By.Id("pay_bills_tab"));
            Assert.AreEqual("Pay Bills", headerName.Text);
        }
        [TestMethod]
        public void Check_If_Table_Exist()
        {
            IWebElement table = _driver.FindElement(By.Id("tabs"));
            Assert.IsNotNull(table);
        }

        [TestMethod]
        public void Get_First_Table_Headers()
        {
            IReadOnlyCollection<IWebElement> tableHeaders = _driver.FindElements(By.CssSelector("#tabs > ul > li"));
            IWebElement header_1= tableHeaders.ElementAt(0);
            Assert.AreEqual("Pay Saved Payee", header_1.Text);
        }

        [TestMethod]
        public void Get_Second_Table_Headers()
        {
            IReadOnlyCollection<IWebElement> tableHeaders = _driver.FindElements(By.CssSelector("#tabs > ul > li"));
            IWebElement header_2 = tableHeaders.ElementAt(1);
            Assert.AreEqual("Add New Payee", header_2.Text);
        }

        [TestMethod]
        public void Get_Third_Table_Headers()
        {
            IReadOnlyCollection<IWebElement> tableHeaders = _driver.FindElements(By.CssSelector("#tabs > ul > li"));
            IWebElement header_3 = tableHeaders.ElementAt(2);
            Assert.AreEqual("Purchase Foreign Currency", header_3.Text);
        }
        [TestMethod]
        public void Check_Count_Table_Header()
        {
            IReadOnlyCollection<IWebElement> tableHeader = _driver.FindElements(By.CssSelector("#tabs > ul > li"));
            Assert.AreEqual(3, tableHeader.Count);
        }

        [TestMethod]
        public void Check_Table_Header_Names()
        {
            IReadOnlyCollection<IWebElement> tableHeader = _driver.FindElements(By.CssSelector("#tabs > ul > li"));
            Assert.AreEqual("Pay Saved Payee", tableHeader.ElementAt(0).Text);
            Assert.AreEqual("Purchase Foreign Currency", tableHeader.ElementAt(2).Text);
        }

        [TestCleanup]
        public void Finish()
        {
            _driver.Quit();
        }
    }
}
