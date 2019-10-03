


using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace API.Helper.Validations.Annotations
{
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class OptionalEmail : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = (string) value;

            var FirstNameproperty = validationContext.ObjectType.GetProperty("FirstName");
            var FirstName = (string)FirstNameproperty.GetValue(validationContext.ObjectInstance, null);

            var LastNameproperty = validationContext.ObjectType.GetProperty("LastName");
            var LastName = (string)FirstNameproperty.GetValue(validationContext.ObjectInstance, null);

            if(String.IsNullOrWhiteSpace(email) && String.IsNullOrWhiteSpace(FirstName) && String.IsNullOrWhiteSpace(LastName)){
                var emailFromUser = validationContext.ObjectType.GetProperty(validationContext.MemberName);
                emailFromUser.SetValue(validationContext.ObjectInstance, null);
                FirstNameproperty.SetValue(validationContext.ObjectInstance, null);
                LastNameproperty.SetValue(validationContext.ObjectInstance, null);
                return null;
            }

            if (!validateChars(LastName))
                return new ValidationResult("The Last name has invalid characters only a-zA-Z0-9-_ is allowed");

            if (!validateChars(FirstName))
                return new ValidationResult("The First name has invalid characters only a-zA-Z0-9-_ is allowed");

            try{
                var validEmail = new MailAddress(email);
                return null;
            }
            catch (Exception) {
                return new ValidationResult("This email is invalid");
            }

            
        }

        private Boolean validateChars(string data) {
            if(data == null)
                return false;
            return new Regex(@"^[a-zA-Z0-9-_]+$").Match(data).Success;
        }
    }


}

