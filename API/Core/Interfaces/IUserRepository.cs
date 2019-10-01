using System.Threading.Tasks;
using API.Core.Domain.Application.Request.ProblemBeta;
using API.Core.Domain.Models;

namespace API.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> SaveUser(string userEmail, PersonalDetail personalDetail = null);
        Task<User> SaveUser(CreateProblemBetaRequest createProblemBetaRequest);
        Task<User> GetWithPersonalDetails(string userId);
    }
}