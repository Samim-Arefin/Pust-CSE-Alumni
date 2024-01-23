using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Alumni.Services.Utilities;
using Alumni.Domain.Enums;
using Alumni.Services.Helper;
using Microsoft.Extensions.Logging;

namespace Alumni.Services.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string id, string userEmail, string token, EmailSubjectEnum subject);
    }

    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailConfirmationAsync(string id, string userEmail, string token, EmailSubjectEnum subject)
        {
            try
            {
                var message = GetMailMessage(id, userEmail, token, subject);
                if (message is not null)
                {
                    using var client = new SmtpClient();
                    await client.ConnectAsync(EmailSettings.Host, EmailSettings.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(EmailSettings.UserName, EmailSettings.MailPassword);
                    var response = await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->SendEmailConfirmationAsync Error->{ex.Message}");
            }
        }

        private MimeMessage GetMailMessage(string id, string userEmail, string token, EmailSubjectEnum subject)
        {
            try
            {
                var message = new MimeMessage
                {
                    Subject = subject switch
                    {
                        EmailSubjectEnum.EmailConfirmation => EnumExtensions.GetDisplayName(EmailSubjectEnum.EmailConfirmation),
                        EmailSubjectEnum.ResetPasswordEmail => EnumExtensions.GetDisplayName(EmailSubjectEnum.ResetPasswordEmail),
                        _ => "System Error"
                    }
                };

                message.From.Add(new MailboxAddress(EmailSettings.Name, EmailSettings.EmailFrom));
                message.To.Add(MailboxAddress.Parse(userEmail));

                var uri = string.Empty;
                var content = string.Empty;

                if(subject is EmailSubjectEnum.EmailConfirmation)
                {
                    uri = $"/verification/{id}?token={token}";
                    content = $@"
                    <p>
                        Please <a href='{AppSettings.Settings.AppUrl + uri}'> click here </a> to confirm your registration!
                    </p>";
                }
                else
                {
                    uri = $"/resetPassword/{id}?token={token}";
                    content = $@"
                    <p>
                        Please <a href='{AppSettings.Settings.AppUrl + uri}'> click here </a> to reset your password!
                    </p>";
                }

                var builder = new BodyBuilder
                {
                    HtmlBody = content.Replace("\n", "<Br>")
                };

                message.Body = builder.ToMessageBody();

                return message;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->GetMailMessage Error->{ex.Message}");
                return null;
            }
        }
    }
}