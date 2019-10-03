using System.Threading.Tasks;
using AutoMapper;
using API.Controllers.Resources.Http.ResponseResources.Users;
using API.Core.Interfaces;
using API.Core.Domain.Models;
using API.Helper.Functions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using API.Controllers.Resources.Http.RequestResources.Users;
using API.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;

        public AuthController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
            _emailSender = emailSender;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestResource loginRequestResource)
        {
            var user = await _unitOfWork.Auth.Login(loginRequestResource.EmailUsername, loginRequestResource.Password);
            if(user == null)
                return BadRequest("Incorrect username or password");

            var userResponseResource = _mapper.Map<UserResponseResource>(user);
            
            return Ok(new {token =  TokenFunctions.generateUserToken(userResponseResource,_config)});
            // return Ok(loginRequestResource);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestResource registerRequestResource)
        {
            var user = _mapper.Map<User>(registerRequestResource);

            _unitOfWork.Auth.Registered += _emailSender.OnUserRegistration;

            user = await _unitOfWork.Auth.Register(user, registerRequestResource.Password, Url, Request);

            return NoContent();
        }

        [HttpGet("identity-available/{user}")]
        public async Task<IActionResult> CheckUser(string user){
            return Ok(await _unitOfWork.Auth.IdentityFree(user));
        }

        // [HttpGet("confirmemail")]
        // public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token){
        //     token = token.Replace(' ', '+');

        //     var userFromDb =  await _unitOfWork.Auth.VerifyEmail(userId, token);

        //     if(userFromDb == null)
        //         return BadRequest("We failed to verify your email kindly request a new link sent");

        //     await _unitOfWork.Complete();

        //     var userResponseResource = _mapper.Map<UserResponseResource>(userFromDb);
            
        //     return Ok(new {token =  TokenFunctions.generateUserToken(userResponseResource,_config)});
        // }

        // [HttpGet("generateresetpassword/{email}")]
        // public async Task<IActionResult> GenerateResetPassword(string email){

        //     var internalUser = await _unitOfWork.Auth.GeneratePasswordResetCode(email);

        //     _emailSender.SendPasswordResetToken(internalUser);

        //     return Ok(new { email });
        // }

        // [HttpPost("resetpassword")]
        // public async Task<IActionResult> ResetPassword(ResetPasswordRequestResource resetPasswordRequestResource){

        //     if(! await _unitOfWork.Auth.ResetUsersPassword(resetPasswordRequestResource))
        //         return BadRequest("Password could not be changed kindly send a new link");

        //     return NoContent();
        // }

        // [HttpGet("resendemailconfirmation/{emailOrId}")]
        // public async Task<IActionResult> ResendEmailConfirmation(string emailOrId){


        //     var internalUser = await _unitOfWork.Auth.GenerateEmailConfirmationDetails(emailOrId);

        //     _emailSender.sendEmailConfirmation(internalUser);

        //     return NoContent();
        // }
    }
}