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
        public IWebElement GetHeaders(int index)
        {
            _common.MyWait(5, By.CssSelector("#tabs > ul > li"));

            IReadOnlyCollection<IWebElement> tableHeaders = _driver.FindElements(By.CssSelector("#tabs > ul > li"));
            return tableHeaders.ElementAt(index);
        }

        [TestMethod]
        public void Schould_First_Tab_Has_Valid_Name()
        {
            ////IReadOnlyCollection<IWebElement> tableHeaders = _driver.FindElements(By.CssSelector("#tabs > ul > li"));
            ////IWebElement header1= tableHeaders.ElementAt(0);
            ////Assert.AreEqual("Pay Saved Payee", header1.Text);
            // Now faster method:

            IWebElement header1 = GetHeaders(0);
            Assert.AreEqual("Pay Saved Payee", header1.Text);

        }

        [TestMethod]
        public void Schould_Second_Tab_Has_Valid_Name()
        {
            IWebElement header2 = GetHeaders(1);
            Assert.AreEqual("Add New Payee", header2.Text);
        }

        [TestMethod]
        public void Schould_Third_Tab_Has_Valid_Name()
        {
            IWebElement header3 = GetHeaders(2);
            Assert.AreEqual("Purchase Foreign Currency", header3.Text);
        }

        [TestMethod]
        public void Check_Count_Tab_Header()
        {
            IReadOnlyCollection<IWebElement> tableHeader = _driver.FindElements(By.CssSelector("#tabs > ul > li"));
            Assert.AreEqual(3, tableHeader.Count);
        }

        [TestMethod]
        public void Check_Tab_Header_Names()
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
