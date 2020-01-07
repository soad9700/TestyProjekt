// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using MailKit.Net.Smtp;
using MimeKit;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Microsoft.Extensions.Options;

namespace QuickApp.Helpers
{
    public interface IEmailSender
    {
        Task<(bool success, string errorMsg)> SendEmailAsync(MailboxAddress sender, MailboxAddress[] recepients,
            string subject, string body, SmtpConfig config = null, bool isHtml = true);

        Task<(bool success, string errorMsg)> SendEmailAsync(string recepientName, string recepientEmail,
            string subject, string body, SmtpConfig config = null, bool isHtml = true);

        Task<(bool success, string errorMsg)> SendEmailAsync(string senderName, string senderEmail,
            string recepientName, string recepientEmail, string subject, string body, SmtpConfig config = null,
            bool isHtml = true);
    }



    public class EmailSender : IEmailSender
    {
        readonly SmtpConfig _config;
        readonly ILogger _logger;
        private readonly SmtpClient _smtpClient;

        public EmailSender(IOptions<AppSettings> config, ILogger<EmailSender> logger, SmtpClient smtpClient)
        {
            _config = config.Value.SmtpConfig;
            _logger = logger;
            _smtpClient = smtpClient;
        }


        public async Task<(bool success, string errorMsg)> SendEmailAsync(
            string recepientName,
            string recepientEmail,
            string subject,
            string body,
            SmtpConfig config = null,
            bool isHtml = true)
        {
            var from = new MailboxAddress(_config.Name, _config.EmailAddress);
            var to = new MailboxAddress(recepientName, recepientEmail);

            return await SendEmailAsync(from, new MailboxAddress[] {to}, subject, body, config, isHtml);
        }



        public async Task<(bool success, string errorMsg)> SendEmailAsync(
            string senderName,
            string senderEmail,
            string recepientName,
            string recepientEmail,
            string subject,
            string body,
            SmtpConfig config = null,
            bool isHtml = true)
        {
            var from = new MailboxAddress(senderName, senderEmail);
            var to = new MailboxAddress(recepientName, recepientEmail);

            return await SendEmailAsync(from, new MailboxAddress[] {to}, subject, body, config, isHtml);
        }


        //For background tasks such as sending emails, its good practice to use job runners such as hangfire https://www.hangfire.io
        //or a service such as SendGrid https://sendgrid.com/
        public async Task<(bool success, string errorMsg)> SendEmailAsync(
            MailboxAddress sender,
            MailboxAddress[] recepients,
            string subject,
            string body,
            SmtpConfig config = null,
            bool isHtml = true)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(sender);
            message.To.AddRange(recepients);
            message.Subject = subject;
            message.Body = isHtml
                ? new BodyBuilder {HtmlBody = body}.ToMessageBody()
                : new TextPart("plain") {Text = body};

            try
            {
                if (config == null)
                    config = _config;

                if (!config.UseSSL)
                    _smtpClient.ServerCertificateValidationCallback = (object sender2, X509Certificate certificate,
                        X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

                await _smtpClient.ConnectAsync(config.Host, config.Port, config.UseSSL).ConfigureAwait(false);
                _smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!string.IsNullOrWhiteSpace(config.Username))
                    await _smtpClient.AuthenticateAsync(config.Username, config.Password).ConfigureAwait(false);

                await _smtpClient.SendAsync(message).ConfigureAwait(false);
                await _smtpClient.DisconnectAsync(true).ConfigureAwait(false);

                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.SEND_EMAIL, ex, "An error occurred whilst sending email");
                return (false, ex.Message);
            }
        }
    }
}
