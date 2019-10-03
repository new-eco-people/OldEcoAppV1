using System.Threading.Tasks;
using API.Controllers.Resources.Http.RequestResources.Comments;
using API.Controllers.Resources.Http.RequestResources.Hacks;
using API.Controllers.Resources.Http.RequestResources.Problems;
using API.Controllers.Resources.Http.ResponseResources.Comments;
using API.Controllers.Resources.Http.ResponseResources.Generic;
using API.Controllers.Resources.Http.ResponseResources.Problems;
using API.Core.Domain.Application.Request.Comment;
using API.Core.Domain.Models.Application.Request.ProblemBeta;
using API.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [AllowAnonymous]
    // [Route("api/[controller]")]
    // [ApiController]
    public class HacksController : ControllerBase
    {
        // private readonly IMapper _mapper;
        // private readonly IUnitOfWork _iUnitOfWork;

        // public HacksController(IMapper mapper, IUnitOfWork IUnitOfWork)
        // {
        //     _mapper = mapper;
        //     _iUnitOfWork = IUnitOfWork;
        // }


        // [HttpGet("countries")]
        // public IActionResult GetCountries() {
        //     // await System.Threading.Tasks.Task.Delay(3000);
        //     return Ok(_iUnitOfWork.Location.getCountries());
        // }

        // [HttpGet("states/{countryId}")]
        // public IActionResult GetStates(int countryId) {
        //     return Ok(_iUnitOfWork.Location.getStates(countryId));
        // }

        // [HttpGet("problems")]
        // public async Task<IActionResult> SearchProblrems([FromQuery] SearchProblemRequestResource filter) {
        //     var appFilter = _mapper.Map<ProblemBetaFilter>(filter);
        //     var problemResource = _mapper.Map<QueryResultResponseResource<ProblemHacksCardResponseResource>>(await _iUnitOfWork.ProbBeta.ProblemBasedOn(appFilter));

        //     return Ok(problemResource);
        // }

        // [HttpGet("comments")]
        // public async Task<IActionResult> GetComments([FromQuery] GetCommentsRequestResource GetCommentsRequestResource) {
        //     var commentFilter = _mapper.Map<CommentFilter>(GetCommentsRequestResource);

        //     var raw = await _iUnitOfWork.Comments.GetComments(commentFilter);

        //     var result = _mapper.Map<QueryResultResponseResource<CommentHacksResponseResource>>(raw);

        //     return Ok(result);
            
        // }

        // [HttpGet("ideas")]
        // public async Task<IActionResult> GetIdeas([FromQuery] GetCommentsRequestResource GetCommentsRequestResource) {
        //     var commentFilter = _mapper.Map<CommentFilter>(GetCommentsRequestResource);

        //     var raw = await _iUnitOfWork.Comments.GetIdeas(commentFilter);

        //     var result = _mapper.Map<QueryResultResponseResource<CommentHacksResponseResource>>(raw);

        //     return Ok(result);
        // }

    }
}