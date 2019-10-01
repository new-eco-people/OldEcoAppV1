using System.Collections.Generic;
using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace API.Helper.Validations.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class isIcoAvaliable : ValidationAttribute
    {
        readonly ICollection<string> _ico  = new List<string>() 
                                                {
                                                    "i", 
                                                    "c", 
                                                    "o"
                                                };

        public override bool IsValid(object value)
        {
            var network = (String)value;

            if(String.IsNullOrEmpty(network)) {
                return true;
            }

            bool result = false;
            if (_ico.Contains(network))
            {
                result = true;
            }
            return result;
        }
    }
}