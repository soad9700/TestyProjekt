using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using QuickApp.Helpers;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace QuickApp.Tests.Helpers
{
    public class EmailSenderTests
    {
        private EmailSender SUT;
        private IOptions<AppSettings> _config;
        private Mock<ILogger<EmailSender>> _loggerMock;
        private Mock<SmtpClient> _smtpClientMock;


        [SetUp]
        public void Setup()
        {
            _config = Options.Create(new AppSettings());
            _config.Value.SmtpConfig = new SmtpConfig();
            _config.Value.SmtpConfig.Name = "QuickApp Template";
            _config.Value.SmtpConfig.EmailAddress = "quickapp@ebenmonney.com";
            _config.Value.SmtpConfig.Host = "mail.ebenmonney.com";
            _config.Value.SmtpConfig.Port = 25;
            _config.Value.SmtpConfig.UseSSL = false;
            _config.Value.SmtpConfig.Username = "quickapp@ebenmonney.com";
            _config.Value.SmtpConfig.Password = "tempP@ss123";
            _loggerMock = new Mock<ILogger<EmailSender>>();
            _smtpClientMock = new Mock<SmtpClient>();
            SUT = new EmailSender(_config, _loggerMock.Object, _smtpClientMock.Object);
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

            _config.Value.SmtpConfig.Host = null;

            SUT = new EmailSender(_config, _loggerMock.Object, _smtpClientMock.Object);

            var expectedResult = (false, "Value cannot be null. (Parameter 'host')");

            var result = await SUT.SendEmailAsync(senderName, senderEmail,
                recepientName, recepientEmail, subject, body);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task SendEmailAsync_OverLoadTwo_InvokedWithHostNull_ShouldReturnErrorMessageInTuple()
        {
            var recepientName = "testName2";
            var recepientEmail = "testEmail2@gmail.com";
            var subject = "testSubject";
            var body = "testEmailBody";

            _config.Value.SmtpConfig.Host = null;

            SUT = new EmailSender(_config, _loggerMock.Object, _smtpClientMock.Object);

            var expectedResult = (false, "Value cannot be null. (Parameter 'host')");

            var result = await SUT.SendEmailAsync(recepientName, recepientEmail, subject, body);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task SendEmailAsync_OverloadThree_InvokedWithHostNull_ShouldReturnErrorMessageInTuple()
        {
            var senderAdress = new MailboxAddress("testName", "testEmail@gmail.com");
            var recepientsAddresses = new MailboxAddress[] {new MailboxAddress("testName", "testEmail@gmail.com")};
            var subject = "testSubject";
            var body = "testEmailBody";

            _config.Value.SmtpConfig.Host = null;

            SUT = new EmailSender(_config, _loggerMock.Object, _smtpClientMock.Object);

            var expectedResult = (false, "Value cannot be null. (Parameter 'host')");

            var result = await SUT.SendEmailAsync(senderAdress, recepientsAddresses, subject, body);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task SendEmailAsync_OverloadOne_InvokedWithTestCredentials_ShouldReturnAddressError()
        {
            var senderName = "testName";
            var senderEmail = "testEmail@gmail.com";
            var recepientName = "testName2";
            var recepientEmail = "testEmail2@gmail.com";
            var subject = "testSubject";
            var body = "testEmailBody";


            var result = await SUT.SendEmailAsync(senderName, senderEmail,
                recepientName, recepientEmail, subject, body);

            Assert.That(result.errorMsg.Length != 0);
        }

        [Test]
        public async Task SendEmailAsync_OverLoadTwo_InvokedWithTestCredentials_ShouldReturnAddressError()
        {
            var recepientName = "testName2";
            var recepientEmail = "testEmail2@gmail.com";
            var subject = "testSubject";
            var body = "testEmailBody";

            string message = null;

            var expectedResult = (false, "Object reference not set to an instance of an object.");

            var result = await SUT.SendEmailAsync(recepientName, recepientEmail, subject, body);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task SendEmailAsync_OverloadThree_InvokedWithTestCredentials_ShouldReturnAddressError()
        {
            var senderAdress = new MailboxAddress("testName", "testEmail@gmail.com");
            var recepientsAddresses = new MailboxAddress[] {new MailboxAddress("testName", "testEmail@gmail.com")};
            var subject = "testSubject";
            var body = "testEmailBody";

            var result = await SUT.SendEmailAsync(senderAdress, recepientsAddresses, subject, body);

            Assert.That(result.errorMsg.Length != 0);
        }

        [Test]
        public async Task SendEmailAsync_OverloadThree_SmtpClientThrowsException_ShouldReturnErrorResult()
        {
            var senderAdress = new MailboxAddress("testName", "testEmail@gmail.com");
            var recepientsAddresses = new MailboxAddress[] {new MailboxAddress("testName", "testEmail@gmail.com")};
            var subject = "testSubject";
            var body = "testEmailBody";

            var expectedResult = (false, "Object reference not set to an instance of an object.");

            _smtpClientMock.Setup(m =>
                    m.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>()))
                .Throws(new Exception());

            var result = await SUT.SendEmailAsync(senderAdress, recepientsAddresses, subject, body);

            Assert.AreEqual(expectedResult, result);
        }

    }
}
