using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MimeKit;

namespace DickinsDev.Utilities.Smtp
{
    public interface ISmtpClient
    {
        Task<int> SendEmailAsync(SmtpMessage smtpMessage);
        void OverrideFromAddress(string DisplayName, string EmailAddress);
    }
    public class SmtpClient : ISmtpClient
    {
        private readonly SmtpConfiguration _smtpConfiguration;
        private readonly ILogger _logger;
        private MailboxAddress FromAddress;

        public SmtpClient(SmtpConfiguration smtpConfiguration, ILogger<SmtpClient> logger)
        {
            _smtpConfiguration = smtpConfiguration;
            _logger = logger;

            FromAddress = MailboxAddress.Parse(smtpConfiguration.Username);
        }

        public void OverrideFromAddress(string DisplayName, string EmailAddress)
        {
            this.FromAddress = new MailboxAddress(DisplayName, EmailAddress);
        }

        public async Task<int> SendEmailAsync(SmtpMessage smtpMessage)
        {
            _logger.LogInformation("SendEmailAsync::{0}::Subject '{1}'", smtpMessage.Guid, smtpMessage.Subject);
            _logger.LogInformation("SendEmailAsync::{0}::Recipients '{1}'", smtpMessage.Guid, string.Join(",", smtpMessage.ToEmail));
            _logger.LogInformation("SendEmailAsync::{0}::UnstructedCerts '{1}'", smtpMessage.Guid, _smtpConfiguration.AllowUntrustedCertificates);
            try
            {
                using (MailKit.Net.Smtp.SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtpClient.CheckCertificateRevocation = _smtpConfiguration.AllowUntrustedCertificates;
                    
                    if (_smtpConfiguration.UseSsl && _smtpConfiguration.Port == 587)
                        smtpClient.Connect(_smtpConfiguration.Hostname,
                                                     _smtpConfiguration.Port,
                                                     MailKit.Security.SecureSocketOptions.StartTls);
                    else if (_smtpConfiguration.UseSsl)
                        smtpClient.Connect(_smtpConfiguration.Hostname,
                                                    _smtpConfiguration.Port,
                                                    MailKit.Security.SecureSocketOptions.SslOnConnect);
                    else
                        smtpClient.Connect(_smtpConfiguration.Hostname,
                                                     _smtpConfiguration.Port);

                    if (_smtpConfiguration.RequireAuthentication)
                        smtpClient.Authenticate(_smtpConfiguration.Username, _smtpConfiguration.Password);

                    await smtpClient.SendAsync(smtpMessage.GetMimeMessage(FromAddress));

                    await smtpClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("SendEmailAsync::{0}::Failure '{1}'", smtpMessage.Guid, ex.Message);
                return ex.HResult;
            }

            return 0;
        }
    }
}
