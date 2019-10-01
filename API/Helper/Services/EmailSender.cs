using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using API.Controllers.Resources.Internal;
using API.Core.Domain.Models;
using API.Core.Interfaces.IServices;
using API.Helper.EventArgs;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace API.Helper.Services
{

    public class EmailSender : IEmailSender
    {

        private readonly string FrontEndDomainName;
        private readonly string SendGridApiKey;
        

        public EmailSender(IConfiguration config)
        {
            SendGridApiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
        }

        public Task OnUserRegistration(object source, EmailArgs emailArgs)
        {
            throw new NotImplementedException();
        }

        public async Task SendEmailAsync(SendGridMessage msg, VerifyEmailObject data)
        {
            var client = new SendGridClient(SendGridApiKey);

            msg.SetTemplateData(data);

            await client.SendEmailAsync(msg);
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }

        public async Task SendWelcomeEmail(string token, User user) {
            var templateId = "d-b675cd85a0aa4e84bab5929c3a015ea2";
            var msg = EmailFunctions.GenerateMsg("norepy@newecopeople.com", "ECO Team", templateId, user.Email);

            var data = new VerifyEmailObject() {
                FirstName = "there"
            };

            await SendEmailAsync(msg, data);
        }

    }

}