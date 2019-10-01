using System.Linq;
using System.Threading.Tasks;
using API.Core.Domain.Application.Request.Comment;
using API.Core.Domain.Models;
using API.Core.Domain.Models.Application.Response;
using API.Core.Interfaces;
using API.Helper.Extension;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }

        public async Task<QueryResult<Comment>> GetComments(CommentFilter filter) {
            var result = new QueryResult<Comment>();

            var query = _context.Comment
                            .Include(c => c.User)
                                .ThenInclude(u => u.PersonalDetail)
                            .Where(c => c.ProblemBetaId.ToString() == filter.ProblemBetaId)
                            .OrderByDescending(c => c.DateCreated)
                            .AsQueryable();

                result.TotalItems = await query.CountAsync();

                query = query.AddPagination(filter);

                result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<QueryResult<Idea>> GetIdeas(CommentFilter filter) {
            var result = new QueryResult<Idea>();

            var query = _context.Idea
                            .Include(c => c.User)
                                .ThenInclude(u => u.PersonalDetail)
                            .Where(c => c.ProblemBetaId.ToString() == filter.ProblemBetaId)
                            .OrderByDescending(c => c.DateCreated)
                            .AsQueryable();

                result.TotalItems = await query.CountAsync();

                query = query.AddPagination(filter);

                result.Items = await query.ToListAsync();

            return result;
        }
    }
}