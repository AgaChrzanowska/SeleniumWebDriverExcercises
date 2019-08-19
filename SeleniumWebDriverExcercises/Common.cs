using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebDriverExcercises
{
    class Common
    {
        private IWebDriver _driver;
        public Common(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Login()
        {
            string url = "http://zero.webappsecurity.com/login.html";

            _driver.Navigate().GoToUrl(url);

            var loginTextBox = _driver.FindElement(By.Id("user_login"));
            loginTextBox.SendKeys("username");

            var passwordTextBox = _driver.FindElement(By.Id("user_password"));
            passwordTextBox.SendKeys("password");

            var btnSubmitEl = _driver.FindElement(By.Name("submit"));
            btnSubmitEl.Click();
        }
        public void MyWait(int seconds, By by)
        {
            WebDriverWait waiter = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }
    }
}
