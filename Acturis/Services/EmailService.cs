using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Text;
using FluentEmail.Razor;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Acturis.Interfaces;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Acturis.Services
{
    public class EmailService : IEmailService
    {

        private readonly SmtpSetting smtpSetting;
        private readonly ILogger<IEmailService> _logger;

        public EmailService(IOptions<SmtpSetting> options, ILogger<IEmailService> logger )
        {
            smtpSetting = options.Value;
            _logger = logger;
        }


        public async Task ReportUnsuccessfulUpload(dynamic member, string error )
        {


            Email.DefaultSender = GetSmtpSender();
            Email.DefaultRenderer = new RazorRenderer();

            var template = new StringBuilder();


            template.AppendLine("The following member was not uploaded:");
            template.AppendLine(
                 "<p>ConNumber: @Model.Contact.ContactNumber </p>" +
                 "<p>First name: @Model.Contact.FirstName </p>" +
                 "<p>Last name: @Model.Contact.LastName</p>" +
                  $"<p>Error: {error}</p>" 
                  
                  );


            try
            {
                var email = await Email
             .From(smtpSetting.From)
             .To(smtpSetting.To)
             .CC(smtpSetting.CC)
             .Subject(smtpSetting.Subject)
             .UsingTemplate(template.ToString(), member)
             .SendAsync();

            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }



        }

        public async Task Report(string report)
        {


            Email.DefaultSender = GetSmtpSender();
            Email.DefaultRenderer = new RazorRenderer();     


            try
            {
                var email = await Email
             .From(smtpSetting.From)
             .To(smtpSetting.To)
             .CC(smtpSetting.CC)
             .Subject(smtpSetting.Subject)
             .Body(report)
             .SendAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }



        }


        private SmtpSender GetSmtpSender()
        {
            var sender = new SmtpSender(() => new SmtpClient(smtpSetting.SmtpHost)

            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential()
                {
                    UserName = smtpSetting.SmtpUser,
                    Password = smtpSetting.SmtpPassword,

                },
                Port = smtpSetting.SmtpPort,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,

            });

            return sender;
        }

    }

}
