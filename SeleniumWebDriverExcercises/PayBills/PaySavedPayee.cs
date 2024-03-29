﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SeleniumWebDriverExcercises.PayBills
{
    [TestClass]
    public class PaySavedPayee
    {
        private static IWebDriver _driver = new ChromeDriver();
        private Common _common = new Common(_driver);

        [TestInitialize]
        public void SetUp()
        {
            _common.Login();
            IWebElement btnPayBills = _driver.FindElement(By.Id("pay_bills_tab"));
            btnPayBills.Click();
            IReadOnlyCollection<IWebElement> tableHeaders = _driver.FindElements(By.CssSelector("#tabs > ul > li"));
            IWebElement header1 = tableHeaders.ElementAt(0);
            header1.Click();
        }

        [TestMethod]
        public void Check_H2_Name()
        {
            IWebElement h2Text = _driver.FindElement(By.ClassName("board-header"));
            Assert.AreEqual("Make payments to your saved payees", h2Text.Text);
        }

        [TestMethod]
        public void Check_If_Table_PSP_Exist()
        {
            IWebElement tablePSP = _driver.FindElement(By.ClassName("board-content"));
            Assert.IsNotNull(tablePSP);
        }

        [TestMethod]
        public void Get_Count_Rows_From_Table()
        {
           IReadOnlyCollection<IWebElement> tableRows = _driver.FindElements(By.ClassName("control-group"));
           Assert.AreEqual(5, tableRows.Count());
        }

        [TestMethod]
        public void Get_Name_Table_Rows()
        {
            IReadOnlyCollection<IWebElement> tableRows = _driver.FindElements(By.ClassName("control-label"));
            Assert.AreEqual("Account", tableRows.ElementAt(1).Text);
        }

        [TestMethod]
        public void Get_Payee_List()
        {
           IReadOnlyCollection<IWebElement> payeeList = _driver.FindElement(By.Id("sp_payee")).FindElements(By.TagName("option"));
           Assert.AreEqual(4, payeeList.Count());
        }


        [TestCleanup]
        public void Finish()
        {
            _driver.Quit();
        }
    }
}
