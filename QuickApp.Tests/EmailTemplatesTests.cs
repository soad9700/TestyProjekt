using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Moq;
using NUnit.Framework;
using QuickApp.Helpers;

namespace QuickApp.Tests
{
    [TestFixture]
    public class EmailTemplatesTests
    {
        private Mock<IWebHostEnvironment> _webHostEnvMock;
        
        [SetUp]
        public void SetUp()
        {
            _webHostEnvMock = new Mock<IWebHostEnvironment>();
            
            _webHostEnvMock.Object.ContentRootFileProvider = new CompositeFileProvider();
        }

        [Test]
        public void ReadPhysicalFile_WithNotExistPath_ShouldThrowsException()
        {
            string emptyPath = "/not/found";

            Assert.That(() => EmailTemplates.ReadPhysicalFile(emptyPath), Throws.Exception);
        }
        
        [Test]
        public void GetTestEmail_ForRecipentName_ShouldReplaceUser()
        {
            EmailTemplates.Initialize(_webHostEnvMock.Object);
            string recipentName = "TestRecipent";
            DateTime testDate = new DateTime(2020, 1, 1);
            string textToReplace = "{user}";

            string emailMessage = EmailTemplates.GetTestEmail(recipentName, testDate);

            bool result = emailMessage.Contains(textToReplace);

            Assert.IsFalse(result);
        }
        
        [Test]
        public void GetTestEmail_NotInitialized_ShouldThrowsInvalidOperationException()
        {
            string recipentName = "TestRecipent";
            DateTime testDate = new DateTime(2020, 1, 1);

            Assert.That(() => EmailTemplates.GetTestEmail(recipentName, testDate), Throws.InvalidOperationException);
        }
        
        [Test]
        public void GetTestEmail_ForTestDate_ShouldReplaceDate()
        {
            
        }
        
        [Test]
        public void GetPlainTextTestEmail_WhenInvoke_ShouldReturnEmailMessage()
        {
           
        }
    }
}