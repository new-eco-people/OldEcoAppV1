using System;
using System.ComponentModel.DataAnnotations;

namespace API.Helper.Validations.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NotNull : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if(value == null) 
                return new ValidationResult("");
            // var digit = 0;
            // var result = int.TryParse((string)value, out digit);
            var data = (int?) value;

            if(!data.HasValue)
                return new ValidationResult("");

            return null;
            // var s = (int)value;
            // throw new Exception((string)value);
            // return null;
        }
    }
}