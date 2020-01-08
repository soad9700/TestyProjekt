using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace QuickApp.Tests.Selenium_login
{
    [TestFixture]
    public class Selenium_LoginFormTests
    {
        private IWebDriver _webDriver;
        private string _loginPage;

        [SetUp]
        public void SetUp()
        {
            _webDriver = new FirefoxDriver("/Users/Jakub/Documents");
            _loginPage = "https://www.ebenmonney.com/wp-login.php";
        }

        [Test]
        public void LoginForm_WithCorrectEmailAddressAndPassword_ShouldRedirectToMyAccountPage()
        {
            string email = "cowowav395@seomail.top";
            string password = "cJpzEY4GUBi#p6T!";
            string redirectPage = "https://www.ebenmonney.com/my-account/";

            _webDriver.Navigate().GoToUrl(_loginPage);

            _webDriver.FindElement(By.Id("user_login")).SendKeys(email);
            _webDriver.FindElement(By.Id("user_pass")).SendKeys(password);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(500));

            _webDriver.FindElement(By.Id("wp-submit")).Click();

            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));

            string pageAfterLogin = _webDriver.Url;

            Assert.AreEqual(redirectPage, pageAfterLogin);
        }

        [Test]
        public void LoginForm_WithCorrectUserNameAndPassword_ShouldRedirectToMyAccountPage()
        {
            string userName = "testSelenium";
            string password = "cJpzEY4GUBi#p6T!";
            string redirectPage = "https://www.ebenmonney.com/my-account/";

            _webDriver.Navigate().GoToUrl(_loginPage);

            _webDriver.FindElement(By.Id("user_login")).SendKeys(userName);
            _webDriver.FindElement(By.Id("user_pass")).SendKeys(password);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(500));

            _webDriver.FindElement(By.Id("wp-submit")).Click();

            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));

            string pageAfterLogin = _webDriver.Url;

            Assert.AreEqual(redirectPage, pageAfterLogin);
        }

        [Test]
        public void LoginForm_LogInClickWithEmptyUserNameAndPassword_ShouldDisplayUsernameFieldIsEmptyAndPasswordFieldIsEmptyMessage()
        {
            string userNameErrorMessage = "The username field is empty.";
            string passwordErrorMessage = "The password field is empty.";

            _webDriver.Navigate().GoToUrl(_loginPage);

            _webDriver.FindElement(By.Id("wp-submit")).Click();

            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));

            bool isLoginErrorMessageDisplayed = _webDriver.FindElement(By.Id("login_error")).Displayed;
            string result = _webDriver.FindElement(By.Id("login_error")).Text;

            bool isUserNameMessageDisplayed = result.Contains(userNameErrorMessage);
            bool isPasswordMessageDisplayed = result.Contains(passwordErrorMessage);

            Assert.IsTrue(isLoginErrorMessageDisplayed);
            Assert.IsTrue(isUserNameMessageDisplayed);
            Assert.IsTrue(isPasswordMessageDisplayed);
        }

        [Test]
        public void LoginForm_ProvidingUnknownUsername_ShouldDisplayUnknownUsernameMessage()
        {
            string unknownUsername = "5a510eb86a4e4ee5b5f9e9acc7f7dc03";
            string password = "test123";
            string expectedMessage = "Unknown username. Check again or try your email address.";

            _webDriver.Navigate().GoToUrl(_loginPage);

            _webDriver.FindElement(By.Id("user_login")).SendKeys(unknownUsername);
            _webDriver.FindElement(By.Id("user_pass")).SendKeys(password);

            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(500));

            _webDriver.FindElement(By.Id("wp-submit")).Click();

            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));

            bool isLoginErrorMessageDisplayed = _webDriver.FindElement(By.Id("login_error")).Displayed;
            string result = _webDriver.FindElement(By.Id("login_error")).Text;

            bool isContainsExpectedMessage = result.Contains(expectedMessage);

            Assert.IsTrue(isContainsExpectedMessage);
            Assert.IsTrue(isLoginErrorMessageDisplayed);
        }

        [Test]
        public void LoginForm_ProvidingWrongPassword_ShouldDisplayIncorrectPasswordMessageForProvidedAccount()
        {
            string userName = "testSelenium";
            string password = "wrongPassword";
            string errorMessage = "The password you entered for the username";
            string ErrorMessageAfterUserName = "is incorrect.";

            _webDriver.Navigate().GoToUrl(_loginPage);

            _webDriver.FindElement(By.Id("user_login")).SendKeys(userName);
            _webDriver.FindElement(By.Id("user_pass")).SendKeys(password);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(500));

            _webDriver.FindElement(By.Id("wp-submit")).Click();

            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));

            string result = _webDriver.FindElement(By.Id("login_error")).Text;
            bool resultContainsErrorMessage = result.Contains(errorMessage);
            bool resultContainsErrorMessageAfterUserName = result.Contains(ErrorMessageAfterUserName);
            bool resultContainsUserName = result.Contains(userName);

            Assert.IsTrue(resultContainsErrorMessage);
            Assert.IsTrue(resultContainsErrorMessageAfterUserName);
            Assert.IsTrue(resultContainsUserName);
        }

        [Test]
        public void LoginForm_WithIncorrectUserNameAndPassword_ShouldStayOnTheSamePage()
        {
            string userName = "testSelenium";
            string password = "wrongPassword";
            string browserWindowTitle = "Log In ‹ Eben Monney — WordPress";

            _webDriver.Navigate().GoToUrl(_loginPage);

            _webDriver.FindElement(By.Id("user_login")).SendKeys(userName);
            _webDriver.FindElement(By.Id("user_pass")).SendKeys(password);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(500));

            _webDriver.FindElement(By.Id("wp-submit")).Click();

            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));

            string currentPageUrl = _webDriver.Url;
            string currentpageTitle = _webDriver.Title;

            Assert.AreEqual(_loginPage, currentPageUrl);
            Assert.AreEqual(browserWindowTitle, currentpageTitle);
        }

        [TearDown]
        public void CloseBrowser()
        {
            _webDriver.Close();
        }
    }
}