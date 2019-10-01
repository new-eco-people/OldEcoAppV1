using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace API.Helper.Validations.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class IsEcoClassAvaliable : ValidationAttribute
    {
        readonly ICollection<string> _networks  = new List<string>() 
                                                {
                                                    "e", 
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
            if (_networks.Contains(network))
            {
                result = true;
            }
            return result;
        }
    }
}