


using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Policy;
using System.Threading.Tasks;

namespace API.Helper.Validations.Annotations
{
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class OptionalUrl : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var url = (string) value;

            if(String.IsNullOrWhiteSpace(url)){
                
                var projectLink = validationContext.ObjectType.GetProperty(validationContext.MemberName);
                projectLink.SetValue(validationContext.ObjectInstance, null);
                return null;
            }

            return (UrlIsValid(url)) ? null :  new ValidationResult("");
        }


        private bool UrlIsValid(string smtpHost)
        {
            bool br = Uri.IsWellFormedUriString(smtpHost, UriKind.RelativeOrAbsolute);
            // try {
            //     IPHostEntry ipHost = Dns.GetHostEntry(smtpHost);
            //     br = true;
            // }
            // catch (SocketException) {
            //     br = false;
            // }
            return br;
        }


    }


}

