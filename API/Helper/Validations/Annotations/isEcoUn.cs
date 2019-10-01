using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Helper.Validations.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class isEcoUn : ValidationAttribute
    {
        private readonly Dictionary<string, ICollection<string>>  EcoUn = new Dictionary<string, ICollection<string>>() {
            {"e", new List<string>() {"clean water and sanitation", "affordable and clean energy", "climate action", "life below water", "life on land", "responsible consumption and production" }},
            {"c", new List<string>() {"no poverty", "zero hunger", "good health and well being", "quality education", "sustainable cities and communities", "peace, justice and strong institutions"}},
            {"o", new List<string>() {"gender equality", "decent work and economic growth", "industry, innovation and infrastructure", "reduce inequalities", "partnerships for the goals"}}
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Check if goals was entered
            var UN_Goals = (String) value;

            if(String.IsNullOrWhiteSpace(UN_Goals))
                return null;

            var property = validationContext.ObjectType.GetProperty("Eco");
            
            var eco = (string)property.GetValue(validationContext.ObjectInstance, null);

            var result = EcoUn.GetValueOrDefault(eco).Contains(UN_Goals.ToLower());

            return result ? null : new ValidationResult("You selected and Invalid UN goal");
        }
    }
}