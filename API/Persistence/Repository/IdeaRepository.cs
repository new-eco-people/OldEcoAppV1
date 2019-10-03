using API.Core.Domain.Models;
using API.Core.Interfaces;

namespace API.Persistence.Repository
{
    public class IdeaRepository : Repository<Idea>, IIdeaRepository
    {
        public IdeaRepository(DataContext context) : base(context)
        {
        }
    }
}