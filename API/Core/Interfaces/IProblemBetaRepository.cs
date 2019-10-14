using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Controllers.Resources.Http.RequestResources.Problems;
using API.Controllers.Resources.Http.ResponseResources.Problems;
using API.Core.Domain.Application.Request.ProblemBeta;
using API.Core.Domain.Models;
using API.Core.Domain.Models.Application.Request.ProblemBeta;
using API.Core.Domain.Models.Application.Response;
using AutoMapper;

namespace API.Core.Interfaces
{
    public interface IProblemBetaRepository : IRepository<ProblemBeta>
    {
        Task<User> saveProblemBeta(ProblemBeta problemBeta, CreateProblemBetaRequest createProblemBetaRequest);

        Task<IEnumerable<ProblemCardRawData>> GetProblemBetaPublic();
        Task<QueryResult<ProblemCardRawData>> ProblemBasedOn(SearchPostFilter filter);
    }
}