using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers.Resources.Http.RequestResources.Problems;
using API.Controllers.Resources.Http.ResponseResources.Generic;
using API.Controllers.Resources.Http.ResponseResources.Problems;
using API.Controllers.Resources.Http.ResponseResources.Users;
using API.Core.Domain.Application.Request.ProblemBeta;
using API.Core.Domain.Models;
using API.Core.Domain.Models.Application.Request.ProblemBeta;
using API.Core.Interfaces;
using API.Helper.Functions;
using API.Helper.Services;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        public ProblemsController(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateProblemBeta(CreateProblemBeteRequestResource createProblemBeteRequestResource) {

            
            var ApplicationProblemBeta = _mapper.Map<CreateProblemBetaRequest>(createProblemBeteRequestResource);
            var user = await _unitOfWork.ProbBeta.saveProblemBeta(_mapper.Map<ProblemBeta>(ApplicationProblemBeta), ApplicationProblemBeta);            
            await _unitOfWork.Complete();

            if(createProblemBeteRequestResource.RememberMe) {
                var userResponseResource = _mapper.Map<UserResponseResource>(user);
            
                return Ok(new {token =  TokenFunctions.generateUserToken(userResponseResource,_config, true)});
            }
            
            return NoContent();

            // return Ok(_mapper.Map<ProblemBeta>(createProblemBetaRequestResource));
            // var keys = Request.Form;
            // return Ok(createProblemBeteRequestResource.RealImages);
        }

        [AllowAnonymous]
        [HttpGet("public")]
         public async Task<IActionResult> GetProblemBetas() {

            // var result = await _unitOfWork.ProbBeta.saveProblemBeta(_mapper.Map<ProblemBeta>(createProblemBetaRequestResource), createProblemBetaRequestResource);
            
            // await _unitOfWork.Complete();
            
            // return NoContent();
            // var problemResource = _mapper.Map<IEnumerable<ProblemBetaResponseResource>>(_unitOfWork.ProbBeta.GetProblemBetaPublic());
            var problemResource = _mapper.Map<IEnumerable<ProblemCardBetaResponseResource>>(await _unitOfWork.ProbBeta.GetProblemBetaPublic());

            return Ok(problemResource);
        }

        [AllowAnonymous]
        [HttpPost("like-email")]
        public async Task<IActionResult> LikeProblemBeta(LikeProblemBetaRequestResource likeProblemBetaRequestResource) {
            // Save the email  return the Id

            var user = await _unitOfWork.Users.SaveUser(likeProblemBetaRequestResource.EmailAddress);
            var result = await _unitOfWork.Likes.UpdateLike(user.Id, likeProblemBetaRequestResource.ProblemId);

            await _unitOfWork.Complete();

            if(likeProblemBetaRequestResource.RememberMe){
                var userResponseResource = _mapper.Map<UserResponseResource>(user);
            
                return Ok(new {token =  TokenFunctions.generateUserToken(userResponseResource,_config, true), data = result});
            }

            return Ok(new {data = result});

        }

        [HttpPost("like-id")]
        public async Task<IActionResult> LikeProblemBetaWithId(LikeProblemBetaWithIdRequestResource likeProblemBetaWithIdRequestResource) {
            var user = await _unitOfWork.Users.GetEntity(TokenFunctions.GetUserId(User));

            if(user == null)
                return BadRequest("It seems we cannot find you");

            var result = await _unitOfWork.Likes.UpdateLike(user.Id, likeProblemBetaWithIdRequestResource.ProblemId);

            await _unitOfWork.Complete();


            return Ok(new {data = result});

        }
        
        [AllowAnonymous]
        [HttpGet("q")]
        public async Task<IActionResult> SearchProblrems([FromQuery] SearchPostRequestResource filter) {
            // System.Threading.Thread.Sleep(3000);
            // var user = await _unitOfWork.Users.GetEntity(TokenFunctions.GetUserId(User));

            // if(user == null)
            //     return BadRequest("It seems we cannot find you");

            // var result = await _unitOfWork.Likes.UpdateLike(user.Id, likeProblemBetaWithIdRequestResource.ProblemId);

            // await _unitOfWork.Complete();


            // var problemResource = _unitOfWork.ProbBeta.ProblemBasedOn(filter);
            var appFilter = _mapper.Map<SearchPostFilter>(filter);
            var problemResource = _mapper.Map<QueryResultResponseResource<ProblemCardBetaResponseResource>>(await _unitOfWork.ProbBeta.ProblemBasedOn(appFilter));

            return Ok(problemResource);
        }

        [AllowAnonymous]
        [HttpGet("total")]
        public async Task<IActionResult> TotalProblems() {
            return Ok(await _unitOfWork.ProbBeta.TotalItems());
        }

    }
}