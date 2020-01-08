using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace QuickApp.Tests.Selenium
{
    [TestFixture]
    public class RegistrationFormTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = new FirefoxDriver(@"C:\Users\amado\Documents\drivers");
        }

        [Test]
        public void FillRegisterForm_ShouldRegisterNewUserWithRandomData()
        {
            var randomString = Guid.NewGuid().ToString();
            _driver.Navigate().GoToUrl("https://www.ebenmonney.com/wp-login.php?action=register");
            _driver.FindElement(By.Id("user_login")).SendKeys(randomString);
            _driver.FindElement(By.Id("user_email")).SendKeys($"{randomString}@gmail.com");

            new WebDriverWait(_driver, TimeSpan.FromMilliseconds(400));

            _driver.FindElement(By.Id("wp-submit")).Click();

            new WebDriverWait(_driver, TimeSpan.FromMilliseconds(400));

            var errMessage = _driver.FindElement(By.ClassName("message")).Displayed;

            Assert.IsTrue(errMessage);
        }

        [Test]
        public void FillRegisterForm_ShouldDisplayedError()
        {
            _driver.Navigate().GoToUrl("https://www.ebenmonney.com/wp-login.php?action=register");
            _driver.FindElement(By.Id("user_login")).SendKeys("test");
            _driver.FindElement(By.Id("user_email")).SendKeys("test@gmail.com");

            new WebDriverWait(_driver, TimeSpan.FromMilliseconds(400));

            _driver.FindElement(By.Id("wp-submit")).Click();

            new WebDriverWait(_driver, TimeSpan.FromMilliseconds(400));

            var errMessage = _driver.FindElement(By.Id("login_error")).Displayed;

            Assert.IsTrue(errMessage);
        }

        [Test]
        public void FillRegisterForm_WithEmptyEmail_DisplayError()
        {
            _driver.Navigate().GoToUrl("https://www.ebenmonney.com/wp-login.php?action=register");
            _driver.FindElement(By.Id("user_login")).SendKeys("test");

            new WebDriverWait(_driver, TimeSpan.FromMilliseconds(400));

            _driver.FindElement(By.Id("wp-submit")).Click();

            new WebDriverWait(_driver, TimeSpan.FromMilliseconds(400));

            var errMessage = _driver.FindElement(By.Id("login_error")).Displayed;

            Assert.IsTrue(errMessage);
        }

        [Test]
        public void FillRegisterForm_WithEmptyUsername_DisplayError()
        {
            _driver.Navigate().GoToUrl("https://www.ebenmonney.com/wp-login.php?action=register");
            _driver.FindElement(By.Id("user_email")).SendKeys("test@gmail.com");

            new WebDriverWait(_driver, TimeSpan.FromMilliseconds(400));

            _driver.FindElement(By.Id("wp-submit")).Click();

            new WebDriverWait(_driver, TimeSpan.FromMilliseconds(400));

            var errMessage = _driver.FindElement(By.Id("login_error")).Displayed;

            Assert.IsTrue(errMessage);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}
