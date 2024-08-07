﻿using linhkien_donet.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Microsoft.Extensions.Logging;
using linhkien_donet.Models.EmailModels;
using static linhkien_donet.Models.EmailModels.EmailContent;
using _01_WEBAPI.Helper.ApiResults;

namespace linhkien_donet.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSetting mailSettings;

        private readonly ILogger<EmailService> logger;

        public EmailService(IOptions<MailSetting> _mailSettings, ILogger<EmailService> _logger)
        {
            mailSettings = _mailSettings.Value;
            logger = _logger;
            logger.LogInformation("Create SendMailService");
        }

        public async Task<ApiResult<bool>> SendMail(EmailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);

                logger.LogInformation("Error email at- " + emailsavefile);
                logger.LogError(ex.Message);

                return new ApiFailResult<bool>(ex.Message);
            }

            smtp.Disconnect(true);


            logger.LogInformation("Send mail to " + mailContent.To);
            return new ApiSuccessResult<bool>("Send mail to" + mailContent.To + " success");



        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendMail(new EmailContent()
            {
                To = email,
                Subject = subject,
                Body = htmlMessage
            });
        }
    }
}
