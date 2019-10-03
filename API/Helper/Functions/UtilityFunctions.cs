using System;
using System.Collections.Generic;
using System.Linq;
using API.Helper.Services;
using Microsoft.AspNetCore.Identity;

namespace API.Helper.Functions
{
    public class UtilityFunctions
    {
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) 
                opts = new PasswordOptions()
                    {
                        RequiredLength = 8,
                        RequiredUniqueChars = 4,
                        RequireDigit = true,
                        RequireLowercase = true,
                        RequireNonAlphanumeric = true,
                        RequireUppercase = true
                    };
        
            string[] randomChars = new [] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();
        
            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count), 
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);
        
            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);
        
            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);
        
            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);
        
            for (int i = chars.Count; i < opts.RequiredLength 
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count), 
                    rcs[rand.Next(0, rcs.Length)]);
            }
        
            return new string(chars.ToArray());
        }
    }

    public class AppEnvironmentVariables {
        public static string SendGrid = "SENDGRID_APIKEY";
        public static string Cloundinary = "CLOUDINARY_DETAILS";
        public static CloudinarySettings CloudinaryDetails = new CloudinarySettings();
        private static readonly string delimeter = "@";

        public static string ConnectionString = "DefaultConnection";

        public static void ConfigureCloundiary() {
            var cloudinary = Environment.GetEnvironmentVariable(AppEnvironmentVariables.Cloundinary);

            // [0] = name, [1] = Key, [2] = Secret
            var array = new string[3];

            if (!string.IsNullOrEmpty(cloudinary)){
                array = cloudinary.Split(delimeter);
                AppEnvironmentVariables.CloudinaryDetails.Name = array[0];
                AppEnvironmentVariables.CloudinaryDetails.Key = array[1];
                AppEnvironmentVariables.CloudinaryDetails.Secret = array[2];
            } else {
                AppEnvironmentVariables.CloudinaryDetails.Name = " ";
                AppEnvironmentVariables.CloudinaryDetails.Key = " ";
                AppEnvironmentVariables.CloudinaryDetails.Secret = " ";
            }

        }
    }
}