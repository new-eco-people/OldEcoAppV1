using System;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Interfaces;
using API.Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using API.Helper.EventArgs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using API.Controllers.Resources.Http.RequestResources.Users;
using System.Collections.Generic;
using API.Controllers.Resources.Internal;
using API.Helper.Functions;

namespace API.Persistence.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        public event Func<object, EmailArgs, Task> Registered;

        public event EventHandler<EmailArgs> userRegistered;

        protected async Task OnUserRegistered(User user, string code, IUrlHelper url, HttpRequest http) {
            // if(userRegistered != null)
            //     userRegistered(this, new EmailArgs() {
            //         Email = email,
            //         Id = id,
            //         Code = code,
            //         Url = url,
            //         Http = http
            //         });

            Func<object, EmailArgs, Task> handler = Registered;
            
            if (handler == null)
            {
                return;
            }

            Delegate[] invocationList = handler.GetInvocationList();
            Task[] handlerTasks = new Task[invocationList.Length];

            for (int i = 0; i < invocationList.Length; i++)
            {
                handlerTasks[i] = ((Func<object, EmailArgs, Task>)invocationList[i])(this, new EmailArgs() { user = user, Code = code, Url = url,Http = http });
            }

            await Task.WhenAll(handlerTasks);

        }

        public AuthRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<User> Login(string emailUsername, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == emailUsername.ToUpper() || u.Email.ToUpper() == emailUsername.ToUpper());
            
            if(user == null)
                return null;
            
            if(!user.EmailConfirmed)
                throw new Exception("Your email has not been verified");
            
            if(await _userManager.CheckPasswordAsync(user, password))
                return user;

            return null;
        }

        public async Task<User> Register(User user, string password, IUrlHelper Url, HttpRequest http) {
            var result = await _userManager.CreateAsync(user, password);

            if(!result.Succeeded)
                throw new Exception(String.Join("\n", result.Errors.Select(x=>x.Description)));

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            OnUserRegistered(user, code, Url, http).Wait();

            return  user;
        }

        public async Task<Boolean> IdentityFree(string user) {
            var userFromDb = await _userManager.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == user.ToUpper() || u.UserName.ToUpper() == user.ToUpper());

            return userFromDb == null ? true : false;
        }

        public async Task<User> VerifyEmail(string userId, string code) {
            var userFromDb = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if(userFromDb == null)
                return null;

            if(userFromDb.EmailConfirmed) 
                throw new Exception(userFromDb.Email + " has already been verified");

            var result = await _userManager.ConfirmEmailAsync(userFromDb, code);

            if(!result.Succeeded)
                return null;
                // throw new Exception(String.Join("\n", result.Errors.Select(x=>x.Description)));

            userFromDb.EmailConfirmed = true;

            return userFromDb;
        }

        public async Task<InternalUser> GeneratePasswordResetCode(string emailAddress) {
            var userFromDb = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == emailAddress.ToUpper());

            if(userFromDb == null)
                throw new Exception("This email is not part of ECO");

            return new InternalUser() {
                User = userFromDb,
                Token = await _userManager.GeneratePasswordResetTokenAsync(userFromDb)
            };

        }

        public async Task<Boolean> ResetUsersPassword(ResetPasswordRequestResource resetPasswordRequestResource) {
            var userFromDb = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == resetPasswordRequestResource.UserEmail.ToUpper());

            if(userFromDb == null)
                return false;
            
            var result = await _userManager.ResetPasswordAsync(userFromDb,resetPasswordRequestResource.Token, resetPasswordRequestResource.Password);

            if(!result.Succeeded)
                return false;

            return true;

        }

        public async Task<InternalUser> GenerateEmailConfirmationDetails(string emailAddressOrId) {

            var userFromDb = await _userManager.Users
                            .FirstOrDefaultAsync(u => u.NormalizedEmail == emailAddressOrId.ToUpper() || u.Id.ToString() == emailAddressOrId);

            // var userFromDb = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == resetPasswordRequestResource.UserEmail.ToUpper());

            if(userFromDb == null)
                throw new Exception("This email is not part of ECO");

            if(userFromDb.EmailConfirmed)
                throw new Exception("This email is has already been verified");

            return new InternalUser() {
                User = userFromDb,
                Token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb)
            };
        }


        // For beta

        public async Task<User> SaveUserEmail(string emailAddress) {
            var User = await _userManager.Users.Include(u => u.PersonalDetail).FirstOrDefaultAsync(u => u.NormalizedEmail == emailAddress.ToUpper());

            if(User != null)
                return User;

            User = new User() {
                Email = emailAddress,
                UserName = emailAddress,
                PersonalDetail = new PersonalDetail()
            };

            var result = await _userManager.CreateAsync(User,UtilityFunctions.GenerateRandomPassword());

            if(!result.Succeeded)
                throw new Exception("It seems we are having some issue with this problem");

            return User;
        }

    }
}