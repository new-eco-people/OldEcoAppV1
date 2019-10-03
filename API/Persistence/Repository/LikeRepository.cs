using System;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Domain.Models;
using API.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository
{
    public class LikeRepository : Repository<Likes>, ILikeRepository
    {
        public LikeRepository(DataContext context)
            :base(context)
        {
            
        }

        public async Task<int> UpdateLike(Guid userId, string problemBetaId) {

            var problem = await _context.ProblemBeta.FirstOrDefaultAsync(p => p.Id.ToString() == problemBetaId);

            if(problem == null)
                throw new Exception("It seems this problem cannot be found at the moment");

            var likeFromDb = await GetEntityBy(like => like.UserId == userId && like.ProblemBetaId.ToString() == problemBetaId);
        
            if (likeFromDb != null) {
                Remove(likeFromDb);
                return -1;
            }

            var newLike = new Likes() {
                UserId = userId,
                ProblemBetaId = problem.Id
            };

            await Add(newLike);

            return 1;
        }
    }
}