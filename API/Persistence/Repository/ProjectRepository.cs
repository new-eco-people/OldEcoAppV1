using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Interfaces;
using API.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {

        public ProjectRepository(DataContext context)
            : base(context)
        {
        }

        // public List<Project> GetProjectPublic() {
        //     return _context.Projects.ToList();
        // }

        public IEnumerable<Project> GetUsersProjects(string userId) {
            return Find(p => p.UserId.ToString() == userId);
        }

        // public async Task<Project> GetSingleProjects(string projectId) {
        //     return await _context.Projects.FirstOrDefaultAsync(p => p.Id.ToString() == projectId);
        // }

        // public async Task<Project> CreateProjects(Project newProject, User creator) {
        //     newProject.User = creator;
        //     _unitOfWork.Add(newProject);

        //     if(!await _unitOfWork.Complete())
        //         throw new Exception("Failed to create Project, kindly try again later");
            
        //     return newProject;
        // }
    }
}