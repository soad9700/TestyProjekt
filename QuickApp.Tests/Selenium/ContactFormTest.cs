using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace QuickApp.Tests.Selenium
{
    public class ContactFormTest
    {
        private IWebDriver _webDriver;
        private string _testName;
        private string _testSubject;
        private string _testEmail;
        private string _testMessage;

        [SetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver("C:\\Users\\Filip\\Desktop");
            _testName = "TESTNAME";
            _testSubject = "DummySubject";
            _testEmail = "testEmail@gmail.com";
            _testMessage = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores";
        }

        [Test]
        public void FillContactFormWithIncorrectEmail_ErrorShouldBeDisplayed()
        {
            _testEmail = "dummyEmail";
            _webDriver.Navigate().GoToUrl("https://www.ebenmonney.com/contact/");

            _webDriver.FindElement(By.ClassName("wpcf7-form-control")).SendKeys(_testName);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));
            _webDriver.FindElement(By.ClassName("wpcf7-validates-as-email")).SendKeys(_testEmail);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));
            _webDriver.FindElement(By.Name("your-subject")).SendKeys(_testSubject);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));
            _webDriver.FindElement(By.ClassName("wpcf7-textarea")).SendKeys(_testMessage);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(1000));

            _webDriver.FindElement(By.ClassName("wpcf7-submit")).Click();

            Thread.Sleep(5000);
            
            bool inputErrorMsg = _webDriver.FindElement(By.ClassName("wpcf7-response-output")).Displayed;
            bool formErrorMsg = _webDriver.FindElement(By.ClassName("wpcf7-response-output")).Displayed;
            
            Assert.IsTrue(formErrorMsg);
            Assert.IsTrue(inputErrorMsg);
        }
        
        [Test]
        public void FillContactFormWithEmptyName_ErrorShouldBeDisplayed()
        {
            _webDriver.Navigate().GoToUrl("https://www.ebenmonney.com/contact/");

            _webDriver.FindElement(By.ClassName("wpcf7-validates-as-email")).SendKeys(_testEmail);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));
            _webDriver.FindElement(By.Name("your-subject")).SendKeys(_testSubject);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));
            _webDriver.FindElement(By.ClassName("wpcf7-textarea")).SendKeys(_testMessage);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(1000));

            _webDriver.FindElement(By.ClassName("wpcf7-submit")).Click();
            

            Thread.Sleep(5000);
            
            bool inputErrorMsg = _webDriver.FindElement(By.ClassName("wpcf7-not-valid-tip")).Displayed;
            bool formErrorMsg = _webDriver.FindElement(By.ClassName("wpcf7-response-output")).Displayed;
            
            Assert.IsTrue(formErrorMsg);
            Assert.IsTrue(inputErrorMsg);
        }
        
        [Test]
        public void FillContactFormWithEmptyEmail_ErrorShouldBeDisplayed()
        {
            _webDriver.Navigate().GoToUrl("https://www.ebenmonney.com/contact/");

            _webDriver.FindElement(By.ClassName("wpcf7-form-control")).SendKeys(_testName);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));
            _webDriver.FindElement(By.Name("your-subject")).SendKeys(_testSubject);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));
            _webDriver.FindElement(By.ClassName("wpcf7-textarea")).SendKeys(_testMessage);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(1000));

            _webDriver.FindElement(By.ClassName("wpcf7-submit")).Click();
            

            Thread.Sleep(5000);
            
            bool inputErrorMsg = _webDriver.FindElement(By.ClassName("wpcf7-not-valid-tip")).Displayed;
            bool formErrorMsg = _webDriver.FindElement(By.ClassName("wpcf7-response-output")).Displayed;
            
            Assert.IsTrue(formErrorMsg);
            Assert.IsTrue(inputErrorMsg);
        }
        
        [Test]
        public void FillContactFormWithEmptyNameAndEmail_ErrorsShouldBeDisplayed()
        {
            _webDriver.Navigate().GoToUrl("https://www.ebenmonney.com/contact/");

            _webDriver.FindElement(By.Name("your-subject")).SendKeys(_testSubject);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(5000));
            _webDriver.FindElement(By.ClassName("wpcf7-textarea")).SendKeys(_testMessage);
            new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(1000));

            _webDriver.FindElement(By.ClassName("wpcf7-submit")).Click();
            

            Thread.Sleep(5000);
            
            var messages = _webDriver.FindElements(By.ClassName("wpcf7-not-valid-tip"));
            bool formErrorMsg = _webDriver.FindElement(By.ClassName("wpcf7-response-output")).Displayed;

            foreach (var message in messages)
            {
                Assert.IsTrue(message.Displayed);
            }
            Assert.IsTrue(formErrorMsg);
        }

        [TearDown]
        public void CloseBrowser()
        {
            _webDriver.Close();
        }
    }
}