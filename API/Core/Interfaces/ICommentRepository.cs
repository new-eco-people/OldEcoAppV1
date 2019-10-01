using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Core.Domain.Application.Request.Comment;
using API.Core.Domain.Models;
using API.Core.Domain.Models.Application.Response;

namespace API.Core.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
       Task<QueryResult<Comment>> GetComments(CommentFilter filter);

       Task<QueryResult<Idea>> GetIdeas(CommentFilter filter);
    }
}