using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Controllers.Resources.Http.ResponseResources.Users;
using API.Core.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Helper.Functions
{
    public static class TokenFunctions
    {
        
        public static string generateUserToken(UserResponseResource user, IConfiguration _config, Boolean withDetails = false) {
                        // Create token an sent;
            var claims = withDetails ? WithDetails(user) : defaultClaim(user);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
        
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();

            var token = tokenhandler.CreateToken(tokenDescriptor);

            // var data = new {token = tokenhandler.WriteToken(token)};

            return tokenhandler.WriteToken(token);
        }

        
        public static bool IsOwnerOfAccount(string userId, ClaimsPrincipal User) {
            
            return userId == User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        
        public static string GetUserId(ClaimsPrincipal User)
        {
            // if(User.HasClaim(c => c.Properties == ClaimTypes.NameIdentifier))
                return User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // return null;
        }

        private static Claim[] defaultClaim(UserResponseResource user) {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            return claims;
        }

        private static Claim[] WithDetails(UserResponseResource user) {

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("email", user.Email),
            };

            if(user.PersonalDetail != null && user.PersonalDetail.FirstName != null) {
                claims =  new [] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("email", user.Email),
                    new Claim("firstName", user.PersonalDetail.FirstName),
                    new Claim("lastName", user.PersonalDetail.LastName),
                };
            }

            return claims;
        }
        
    }
}