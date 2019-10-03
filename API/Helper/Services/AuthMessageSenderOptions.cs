
using Microsoft.Extensions.Configuration;

namespace API.Helper.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}