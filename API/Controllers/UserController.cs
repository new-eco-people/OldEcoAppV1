using System.Threading.Tasks;
using API.Controllers.Resources.Http.ResponseResources.Users;
using API.Core.Interfaces;
using API.Core.Interfaces.IServices;
using API.Helper.Functions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }


        // [HttpGet]
        // public async Task<IActionResult> GetAllProjects() {
        //     var user = await _unitOfWork.Users.GetEntity(TokenFunctions.GetUserId(User));

        //     if(user == null) 
        //         return BadRequest();

        //     return Ok(_mapper.Map<UserResponseResource>(user));
        // }
    }
}