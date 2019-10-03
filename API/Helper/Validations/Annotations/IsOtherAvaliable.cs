using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Helper.Validations.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IsOtherAvaliable : ValidationAttribute
    {
        private readonly string _targetProperty;
        public IsOtherAvaliable(string PropertyName)
        : base("")
        {
            _targetProperty = PropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_targetProperty);
            var requiredData = (string)property.GetValue(validationContext.ObjectInstance, null);

            var thisProperty = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            var propertyValue = (string)thisProperty.GetValue(validationContext.ObjectInstance, null);

            if(String.IsNullOrWhiteSpace(requiredData) && String.IsNullOrWhiteSpace(propertyValue))
                return new ValidationResult("Kindly choose "+ validationContext.MemberName+ " or other");

            var completeData = new String[2];

            if(!String.IsNullOrWhiteSpace(requiredData))
                completeData[0] = requiredData;
            
            if(!String.IsNullOrWhiteSpace(propertyValue))
                completeData[1] = propertyValue;

            // if(!String.IsNullOrWhiteSpace(requiredData)) {
            //     var ecoUn = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            //     ecoUn.SetValue(validationContext.ObjectInstance, null);

            //     return null;
            // }

            property.SetValue(validationContext.ObjectInstance, String.Join('-', completeData));
            return null;

        }
    }
}