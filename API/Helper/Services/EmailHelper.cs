using Newtonsoft.Json;
using SendGrid.Helpers.Mail;

namespace API.Helper.Services
{
    public class EmailHelper
    {
        
    }

    public class VerifyEmailObject
    {
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public static class EmailFunctions
    {
        public static SendGridMessage GenerateMsg(string from, string fromName, string templateId, string emailAddress) {

            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress(from, fromName));
            msg.AddTo(new EmailAddress(
                emailAddress
            ));

            msg.SetTemplateId(templateId);

            return msg;
        }
    }
}