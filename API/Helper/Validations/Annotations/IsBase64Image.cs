using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Helper.Validations.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IsBase64Image : ValidationAttribute
    {
        
        private readonly string _targetProperty;
        public IsBase64Image(string PropertyName)
        : base("")
        {
            _targetProperty = PropertyName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var s = value as List<string>;

            if(s.Count < 0)
                return null;

            if(s.Count >= 6)
                return new ValidationResult("Number of Pictures uploaded exceeds 5");

            var property = validationContext.ObjectType.GetProperty(_targetProperty);

            var RealImage = new List<byte[]>();

            try{
                s.ForEach(imageString => {
                    var secondpart = imageString.Split(',')[1];
                    RealImage.Add(Convert.FromBase64String(secondpart));
                });
                property.SetValue(validationContext.ObjectInstance, RealImage);
                return null;
            }
            catch(Exception e) {
                return new ValidationResult(e.Message);
            }
            // throw new Exception(s.Count.ToString());
            // return null;
        }
    }
}