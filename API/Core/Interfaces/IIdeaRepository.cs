using System.Threading.Tasks;
using API.Core.Domain.Models;

namespace API.Core.Interfaces
{
    public interface IIdeaRepository : IRepository<Idea>
    {
        // Task<User> saveProblemBeta(ProblemBeta problemBeta, CreateProblemBetaRequest createProblemBetaRequest);

        // Task<IEnumerable<ProblemCardRawData>> GetProblemBetaPublic();
        // Task<QueryResult<ProblemCardRawData>> ProblemBasedOn(ProblemBetaFilter filter);
    }
}