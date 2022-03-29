using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using FluentEmail.Razor;
using Microsoft.Extensions.Options;
using Acturis.Interfaces;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Linq;
using FluentEmail.Core.Models;

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

              var listOfAddress = smtpSetting.CC.Split(";").Select(ea => new Address { EmailAddress = ea }).ToList();
              var email = await Email
             .From(smtpSetting.From)
             .To(smtpSetting.To)
             .CC(listOfAddress)
            
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
            try
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
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
           
        }

    }

}
