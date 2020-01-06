using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using QuickApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuickApp.Tests.Helpers
{
    public class EmailSenderTests
    {
        private EmailSender SUT;
        private IOptions<AppSettings> _config;
        private Mock<ILogger<EmailSender>> _logger;


        [SetUp]
        public void Setup()
        {
            _config = Options.Create(new AppSettings());
            _config.Value.SmtpConfig = new SmtpConfig();
            _config.Value.SmtpConfig.Name = "testName";
            _config.Value.SmtpConfig.EmailAddress = "testEmailAddress@gmail.com";
            _logger = new Mock<ILogger<EmailSender>>();
            SUT = new EmailSender(_config, _logger.Object);
        }

        [Test]
        public async Task SendEmailAsync_OverloadOne_InvokedWithHostNull_ShouldReturnErrorMessageInTuple()
        {
            var senderName = "testName";
            var senderEmail = "testEmail@gmail.com";
            var recepientName = "testName2";
            var recepientEmail = "testEmail2@gmail.com";
            var subject = "testSubject";
            var body = "testEmailBody";

            var expectedResult = (false, "Value cannot be null. (Parameter 'host')");

            var result = await SUT.SendEmailAsync(senderName, senderEmail,
                recepientName, recepientEmail, subject, body);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task SendEmailAsync_InvokedWithHostNull_ShouldReturnErrorMessageInTuple()
        {
            var recepientName = "testName2";
            var recepientEmail = "testEmail2@gmail.com";
            var subject = "testSubject";
            var body = "testEmailBody";

            var expectedResult = (false, "Value cannot be null. (Parameter 'host')");

            var result = await SUT.SendEmailAsync(recepientName, recepientEmail, subject, body);

            Assert.AreEqual(expectedResult, result);
        }

//        [Test]
//        public async Task SendEmailAsync_InvokedWithHostNull_ShouldReturnErrorMessageInTuple()
//        {
//            var recepientName = "testName2";
//            var recepientEmail = "testEmail2@gmail.com";
//            var subject = "testSubject";
//            var body = "testEmailBody";
//
//            var expectedResult = (false, "Value cannot be null. (Parameter 'host')");
//
//            var result = await SUT.SendEmailAsync(recepientName, recepientEmail, subject, body);
//
//            Assert.AreEqual(expectedResult, result);
//        }
    }
}
