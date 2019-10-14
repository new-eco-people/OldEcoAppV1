using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers.Resources.Http.RequestResources.Ideas;
using API.Controllers.Resources.Http.ResponseResources.IdeaPost;
using API.Controllers.Resources.Http.ResponseResources.Users;
using API.Core.Domain.Application.Request.Idea;
using API.Core.Domain.Models;
using API.Core.Interfaces;
using API.Helper.Functions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
       
    [Route("api/[controller]")]
    [ApiController]
    public class IdeaController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public IdeaController(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateIdea(CreateIdeaRequestResource createIdeaRequestResource) {

            var ideaData = _mapper.Map<CreateIdeaPostRequest>(createIdeaRequestResource);
            var user = await _unitOfWork.IdeaPosts.SaveIdeaPost(_mapper.Map<IdeaPost>(ideaData), ideaData);

            await _unitOfWork.Complete();

            if (createIdeaRequestResource.RememberMe) {
                var userResponseResource = _mapper.Map<UserResponseResource>(user);
            
                return Ok(new {token =  TokenFunctions.generateUserToken(userResponseResource,_config, true)});
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("public")]
         public async Task<IActionResult> GetProblemBetas() {

            // var result = await _unitOfWork.ProbBeta.saveProblemBeta(_mapper.Map<ProblemBeta>(createProblemBetaRequestResource), createProblemBetaRequestResource);
            
            // await _unitOfWork.Complete();
            
            // return NoContent();
            // var problemResource = _mapper.Map<IEnumerable<ProblemBetaResponseResource>>(_unitOfWork.ProbBeta.GetProblemBetaPublic());
            var problemResource = _mapper.Map<IEnumerable<IdeaPostCardResponseResource>>(await _unitOfWork.IdeaPosts.GetIdeaPostPublic());

            return Ok(problemResource);
        }


    }
}