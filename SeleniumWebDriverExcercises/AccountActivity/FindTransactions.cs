using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebDriverExcercises.AccountActivity
{
    [TestClass]
    public class FindTransactions
    {
        private static IWebDriver _driver = new ChromeDriver();
        private Common _common = new Common(_driver);

        [TestInitialize]
        public void SetUp()
        {
            _common.Login();
        }

        [TestMethod]
        public void Check_Headers_Texts()
        {
            string url = "http://zero.webappsecurity.com/bank/account-activity.html?accountId=2";
            _driver.Navigate().GoToUrl(url);

            IWebElement header2 = _driver.FindElement(By.CssSelector("#tabs > ul > li.ui-state-default.ui-corner-top:nth-child(2)"));
            header2.Click();

            IWebElement headerHandler2 = _driver.FindElement(By.XPath("//*[@id=\"tabs\"]/ul/li[2]/a"));
            Assert.AreEqual("Find Transactions", headerHandler2.Text);

            _common.MyWait(3, By.CssSelector("#ui-tabs-2 > p"));

            IReadOnlyCollection<IWebElement> descriptionHandlers = _driver.FindElements(By.ClassName("help-block"));
            IWebElement descriptionHandler2 = descriptionHandlers.ElementAt(1);
            Assert.AreEqual("Complete at least one field below and click Find", descriptionHandler2.Text);
        }
        [TestMethod]
        public void Form_Field()
        {
            string url = "http://zero.webappsecurity.com/bank/account-activity.html";
            _driver.Navigate().GoToUrl(url);

            DoesFormExist();
            FillFormAndSend();
        }
        private void DoesFormExist()
        {
            IWebElement header2 = _driver.FindElement(By.CssSelector("#tabs > ul > li.ui-state-default.ui-corner-top:nth-child(2)"));
            header2.Click();

            _common.MyWait(5, By.Id("find_transactions_form"));

            IWebElement transactionForm = null;

            //W ten sposób sprawdzamy, czy element istnieje. Null oznacza puste przypisanie dla zmiennej referencyjnej.

            bool elementExists = MyElementExists(By.Id("find_transactions_form"));
            if (elementExists == true)
            {
                transactionForm = _driver.FindElement(By.Id("find_transactions_form"));
            }
            else
            {
                Assert.Fail();
            }

            IReadOnlyCollection<IWebElement> rowsForm = transactionForm.FindElements(By.CssSelector("#find_transactions_form div.control-group"));

            Assert.AreEqual(4, rowsForm.Count);
        }
        public bool MyElementExists(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void FillFormAndSend()
        {
            _driver.Manage().Window.Maximize();

            IWebElement fieldDescription = _driver.FindElement(By.Name("description"));
            fieldDescription.SendKeys("ONLINE TRANSFER REF #UKKSDRQG6L");

            IWebElement fieldDates = _driver.FindElement(By.Name("fromDate"));
            fieldDates.SendKeys("2011-09-06");

            IWebElement fieldDatesTo = _driver.FindElement(By.Name("toDate"));
            fieldDatesTo.SendKeys("2012-09-06");

            IWebElement fromAmount = _driver.FindElement(By.Id("aa_fromAmount"));
            fromAmount.SendKeys("300");

            IWebElement toAmount = _driver.FindElement(By.Id("aa_toAmount"));
            //toAmount.Click();
            toAmount.SendKeys("1500");

            IWebElement dropdownType = _driver.FindElement(By.XPath("//*[@id=\"aa_type\"]/option[1]"));
            dropdownType.Click();

            _common.MyWait(5, By.ClassName("btn-primary"));

            //WebDriverWait waiter = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            //waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("btn-primary")));

            IWebElement btnFind = _driver.FindElement(By.ClassName("btn-primary"));
            btnFind.Click();

            _common.MyWait(10, By.Id("filtered_transactions_for_account"));

            IWebElement tableEist = _driver.FindElement(By.Id("filtered_transactions_for_account"));
            Assert.IsTrue(tableEist.Displayed);
        }
        [TestCleanup]
        public void Finish()
        {
            _driver.Quit();
        }
    }
}
