using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers.Resources.Internal;
using API.Core.Domain.Models;
using API.Helper.EventArgs;
using API.Helper.Services;

namespace API.Core.Interfaces.IServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task OnUserRegistration(object source, EmailArgs emailArgs);
        Task SendWelcomeEmail(string token, User user);
    }
}