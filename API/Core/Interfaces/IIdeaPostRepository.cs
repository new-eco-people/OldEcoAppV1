using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers.Resources.Http.ResponseResources.IdeaPost;
using API.Core.Domain.Application.Request.Idea;
using API.Core.Domain.Models;
using API.Core.Domain.Models.Application.Request.ProblemBeta;
using API.Core.Domain.Models.Application.Response;

namespace API.Core.Interfaces
{
    public interface IIdeaPostRepository : IRepository<IdeaPost>
    {
         Task<User> SaveIdeaPost(IdeaPost ideaPost, CreateIdeaPostRequest createIdeaPostRequest);
         Task<IEnumerable<IdeaPostCardRawData>> GetIdeaPostPublic();
         Task<QueryResult<IdeaPostCardRawData>> IdeaPostBasedOn(SearchPostFilter filter );
    }
}