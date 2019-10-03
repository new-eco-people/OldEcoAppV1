using System;
using System.ComponentModel.DataAnnotations;

namespace API.Helper.Validations.Annotations
{
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NullWhiteSpaces : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var thisProperty = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            var propertyValue = (string)thisProperty.GetValue(validationContext.ObjectInstance, null);

            if(String.IsNullOrWhiteSpace(propertyValue))
                thisProperty.SetValue(validationContext.ObjectInstance, null);
                
            return null;

        }
    }
}