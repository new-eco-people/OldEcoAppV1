using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Domain.Models;

namespace API.Core.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> GetUsersProjects(string userId);
        //  List<Project> GetProjectPublic();
        //  List<Project> GetUsersProjects(string userId);
        //  Task<Project> GetSingleProjects(string projectId);
        //  Task<Project> CreateProjects(Project newProject, User creator);
    }
}