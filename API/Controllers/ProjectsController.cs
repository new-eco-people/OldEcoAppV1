using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using API.Controllers.Resources.Http.RequestResources.Projects;
using API.Controllers.Resources.Http.ResponseResources.Projects;
using API.Core.Interfaces;
using API.Core.Domain.Models;
using API.Helper.Functions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        // private readonly IMapper _mapper;
        // private readonly IUnitOfWork _unitOfWork;

        // public ProjectsController(IMapper mapper, IUnitOfWork unitOfWork)
        // {
        //     _mapper = mapper;
        //     _unitOfWork = unitOfWork;
        // }
        
        // // [AllowAnonymous]
        // [HttpGet]
        // public async Task<IActionResult> GetAllProjects() {
        //     var projects = await _unitOfWork.Projects.GetAll();
        //     return Ok(_mapper.Map<IEnumerable<ProjectResponseResource>>(projects));
        // }

        // [HttpGet("user/{userId}")]
        // public IActionResult GetUsersProject(string userId) {
        //     var userProjects = _unitOfWork.Projects.GetUsersProjects(userId);
        //     return Ok(_mapper.Map<IEnumerable<ProjectResponseResource>>(userProjects));
        // }

        // [HttpGet("{projectId}")]
        // public async Task<IActionResult> GetSingleProject(string projectId) {
        //     var project = await _unitOfWork.Projects.GetEntity(projectId);

        //     if(project == null)
        //         return NotFound();
            
        //     return Ok(_mapper.Map<ProjectResponseResource>(project));
        // }

        // [HttpPost("new")]
        // public async Task<IActionResult> CreateProject(NewProjectRequestResource newProjectRequestResource) {
        //     var user = await _unitOfWork.Users.GetEntity(TokenFunctions.GetUserId(User));

        //     if(user == null)
        //         return Unauthorized();

        //     var project = _mapper.Map<Project>(newProjectRequestResource);
        //     project.UserId = user.Id;

        //     await _unitOfWork.Projects.Add(project);
        //     await _unitOfWork.Complete();
            
        //     return Ok(_mapper.Map<ProjectResponseResource>(project));
        // }

        // [HttpPatch("{projectId}")]
        // public async Task<IActionResult> CreateProject([FromBody] EditProjectRequestResource editProjectRequestResource, [FromRoute] string projectId) {
            
        //     var projects = _unitOfWork.Projects.Find(p => p.UserId.ToString() == TokenFunctions.GetUserId(User) && p.Id.ToString() == projectId).ToList();

        //     if(projects.Count == 0)
        //         return NotFound();

        //     var project = projects.First();

        //     _mapper.Map(editProjectRequestResource, project);
        //     await _unitOfWork.Complete();

        //     return Ok(_mapper.Map<ProjectResponseResource>(project));

        // }
    }
}