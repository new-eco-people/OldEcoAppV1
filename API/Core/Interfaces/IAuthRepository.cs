using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers.Resources.Http.RequestResources.Users;
using API.Controllers.Resources.Internal;
using API.Core.Domain.Models;
using API.Helper.EventArgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Login(string emailUsername, string password);
        Task<User> Register(User user, string password, IUrlHelper Url, HttpRequest http);
        Task<Boolean> IdentityFree(string user);
        Task<User> VerifyEmail(string userId, string code);
        event EventHandler<EmailArgs> userRegistered;
        event Func<object, EmailArgs, Task> Registered;
        Task<InternalUser> GeneratePasswordResetCode(string emailAddress);
        Task<Boolean> ResetUsersPassword(ResetPasswordRequestResource resetPasswordRequestResource);
        Task<InternalUser> GenerateEmailConfirmationDetails(string emailAddressOrId);

        Task<User> SaveUserEmail(string emailAddress);

    }
}