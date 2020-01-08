using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Moq;
using NUnit.Framework;
using QuickApp.Helpers;

namespace QuickApp.Tests
{
    [TestFixture]
    public class EmailTemplatesTests
    {
        private Mock<IWebHostEnvironment> _webHostEnvMock;
        private string _fileInfoPath;
            
        
        [SetUp]
        public void SetUp()
        {
            _webHostEnvMock = new Mock<IWebHostEnvironment>();

            //On Windows OS needs to be changed
            _fileInfoPath = "/Users/Jakub/RiderProjects/UnitTestsLabsProject/QuickApp/Helpers/Templates/TestEmail.template";
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
            _webHostEnvMock.DefaultValue = DefaultValue.Mock;
            var provider = _webHostEnvMock.Object.ContentRootFileProvider;
            var providerMock = Mock.Get(provider);
            var fileMock = new PhysicalFileInfo
                (new FileInfo(_fileInfoPath));
            
            providerMock.Setup(x => x.GetFileInfo(It.IsAny<string>()))
                .Returns(fileMock);
            EmailTemplates.Initialize(_webHostEnvMock.Object);
            string recipentName = "TestRecipent";
            DateTime testDate = new DateTime(2020, 1, 1);
            string textToReplace = "{user}";

            string emailMessage = EmailTemplates.GetTestEmail(recipentName, testDate);

            bool result = emailMessage.Contains(textToReplace);

            Assert.IsFalse(result);
        }
        
        [Test]
        public void GetTestEmail_ForTestDate_ShouldReplaceTestDate()
        {
            _webHostEnvMock.DefaultValue = DefaultValue.Mock;
            var provider = _webHostEnvMock.Object.ContentRootFileProvider;
            var providerMock = Mock.Get(provider);
            var fileMock = new PhysicalFileInfo
                (new FileInfo(_fileInfoPath));
            
            providerMock.Setup(x => x.GetFileInfo(It.IsAny<string>()))
                .Returns(fileMock);
            EmailTemplates.Initialize(_webHostEnvMock.Object);
            string recipentName = "TestRecipent";
            DateTime testDate = new DateTime(2020, 1, 1);
            string textToReplace = "{testDate}";

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
        public void GetPlainTextTestEmail_WhenInvoke_ShouldReturnNotNullEmailMessage()
        {
            _webHostEnvMock.DefaultValue = DefaultValue.Mock;
            var provider = _webHostEnvMock.Object.ContentRootFileProvider;
            var providerMock = Mock.Get(provider);
            var fileMock = new PhysicalFileInfo
                (new FileInfo(_fileInfoPath));
            
            providerMock.Setup(x => x.GetFileInfo(It.IsAny<string>()))
                .Returns(fileMock);
            EmailTemplates.Initialize(_webHostEnvMock.Object);
            DateTime testDate = new DateTime(2020, 1, 1);

            string emailMessage = EmailTemplates.GetPlainTextTestEmail(testDate);
            
            Assert.IsNotNull(emailMessage);
        }
    }
}