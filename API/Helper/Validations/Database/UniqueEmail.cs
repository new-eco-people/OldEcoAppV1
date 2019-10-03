using System;
using System.Threading.Tasks;
using API.Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Helper.Validations.Database
{
    public class UniqueEmail<TUser> : IUserValidator<TUser> where TUser : User
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            var userFromRepo = await manager.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(userFromRepo == null)
                return IdentityResult.Success;

            throw new Exception(user.Email +" has already been taken designed by Pere");

            // return IdentityResult.Failed(
            //         new IdentityError() 
            //         { API = "Unique Email", Description = user.Email +" has already been taken"}
            //     );
        }
    }
}