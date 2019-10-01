using System;
using System.ComponentModel.DataAnnotations;

namespace API.Helper.Validations.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ProblemPaging :  ValidationAttribute
    {
        private readonly int _lowest;
        private readonly int _highest;
        private readonly int _defaultValue;

        public ProblemPaging(int Lowest, int Highest, int DefaultValue)
        : base("")
        {
            _lowest = Lowest;
            _highest = Highest;
            _defaultValue = DefaultValue;
        }

         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var thisProperty = validationContext.ObjectType.GetProperty(validationContext.MemberName);

            // if (value == null){
            //     thisProperty.SetValue(validationContext.ObjectInstance, 1);
            //     return null;
            // }

            
            var propertyValue = (int?)thisProperty.GetValue(validationContext.ObjectInstance, null);

            if(propertyValue.HasValue && propertyValue.Value >= _lowest &&  propertyValue.Value <= _highest)
                return null;
                
            thisProperty.SetValue(validationContext.ObjectInstance, _defaultValue);
            return null;

        }
    }
}