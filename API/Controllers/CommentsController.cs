using System.Threading.Tasks;
using API.Controllers.Resources.Http.RequestResources.Comments;
using API.Controllers.Resources.Http.ResponseResources.Comments;
using API.Controllers.Resources.Http.ResponseResources.Generic;
using API.Controllers.Resources.Http.ResponseResources.Users;
using API.Core.Domain.Application.Request.Comment;
using API.Core.Domain.Models;
using API.Core.Interfaces;
using API.Helper.Functions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    // [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IConfiguration _config;

        public CommentsController(IMapper mapper, IUnitOfWork IUnitOfWork, IConfiguration config)
        {
            _mapper = mapper;
            _iUnitOfWork = IUnitOfWork;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddCommentViaEmail(CreateCommentRequestResource createCommentRequestResource) {
            var newComment = _mapper.Map<Comment>(createCommentRequestResource);

            var user = await _iUnitOfWork.Users.SaveUser(createCommentRequestResource.Email);

            var problemBeta = await _iUnitOfWork.ProbBeta.GetEntity(createCommentRequestResource.ProblemId);

            if (problemBeta == null)
                return BadRequest("We could not find the problem to add a comment");

            newComment.User = user;
            newComment.ProblemBeta = problemBeta;

            await _iUnitOfWork.Comments.Add(newComment);
            await _iUnitOfWork.Complete();

            var data = _mapper.Map<CommentResponseResource>(newComment);

            if(createCommentRequestResource.RememberMe){
                var userResponseResource = _mapper.Map<UserResponseResource>(user);
            
                return Ok(new {token =  TokenFunctions.generateUserToken(userResponseResource,_config, true), data});
            }

            return Ok(new {data});
            // return Ok(createCommentRequestResource);
        }

        // [Authorize]
        [HttpPost("id")]
        public async Task<IActionResult> AddCommentViaId(CreateCommentWithIdRequestResource CreateCommentWithIdRequestResource) {
            var newComment = _mapper.Map<Comment>(CreateCommentWithIdRequestResource);

            var user = await _iUnitOfWork.Users.GetWithPersonalDetails(TokenFunctions.GetUserId(User));

            if (user == null)
               return Unauthorized("It seems you are not logged in");

            var problemBeta = await _iUnitOfWork.ProbBeta.GetEntity(CreateCommentWithIdRequestResource.ProblemId);

            if (problemBeta == null)
                return BadRequest("We could not find the problem to add a comment");

            newComment.User = user;
            newComment.ProblemBeta = problemBeta;

            await _iUnitOfWork.Comments.Add(newComment);
            await _iUnitOfWork.Complete();

            return Ok(_mapper.Map<CommentResponseResource>(newComment));
            // return Ok(CreateCommentWithIdRequestResource);
        }


        // --------- ideas
        [AllowAnonymous]
        [HttpPost("idea")]
        public async Task<IActionResult> AddIdeaViaEmail(CreateCommentRequestResource createCommentRequestResource) {
            var newIdea = _mapper.Map<Idea>(createCommentRequestResource);

            var user = await _iUnitOfWork.Users.SaveUser(createCommentRequestResource.Email);

            var problemBeta = await _iUnitOfWork.ProbBeta.GetEntity(createCommentRequestResource.ProblemId);

            if (problemBeta == null)
                return BadRequest("We could not find the problem to add a comment");

            newIdea.User = user;
            newIdea.ProblemBeta = problemBeta;

            await _iUnitOfWork.Ideas.Add(newIdea);
            await _iUnitOfWork.Complete();

            var data = _mapper.Map<CommentResponseResource>(newIdea);

             if(createCommentRequestResource.RememberMe){
                return Ok(new {token =  TokenFunctions.generateUserToken(data.User,_config, true), data});
            }

            return Ok(new {data});
        }

        // Like comment by id
        [HttpPost("idea-id")]
        public async Task<IActionResult> AddIdeaViaId(CreateCommentWithIdRequestResource createCommentWithIdRequestResource) {
            var newIdea = _mapper.Map<Idea>(createCommentWithIdRequestResource);

            var user = await _iUnitOfWork.Users.GetWithPersonalDetails(TokenFunctions.GetUserId(User));

            if (user == null)
               return Unauthorized("It seems you are not logged in");

            var problemBeta = await _iUnitOfWork.ProbBeta.GetEntity(createCommentWithIdRequestResource.ProblemId);

            if (problemBeta == null)
                return BadRequest("We could not find the problem to add a comment");

            newIdea.User = user;
            newIdea.ProblemBeta = problemBeta;

            await _iUnitOfWork.Ideas.Add(newIdea);
            await _iUnitOfWork.Complete();

            var data = _mapper.Map<CommentResponseResource>(newIdea);

            return Ok(data);
        }

        // Get ideas for a comment
        [AllowAnonymous]
        [HttpGet("ideas")]
        public async Task<IActionResult> GetIdeas([FromQuery] GetCommentsRequestResource GetCommentsRequestResource) {
            var commentFilter = _mapper.Map<CommentFilter>(GetCommentsRequestResource);

            var raw = await _iUnitOfWork.Comments.GetIdeas(commentFilter);

            var result = _mapper.Map<QueryResultResponseResource<CommentResponseResource>>(raw);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetComments([FromQuery] GetCommentsRequestResource GetCommentsRequestResource) {
            var commentFilter = _mapper.Map<CommentFilter>(GetCommentsRequestResource);

            var raw = await _iUnitOfWork.Comments.GetComments(commentFilter);

            var result = _mapper.Map<QueryResultResponseResource<CommentResponseResource>>(raw);

            return Ok(result);
            
        }
    }
}