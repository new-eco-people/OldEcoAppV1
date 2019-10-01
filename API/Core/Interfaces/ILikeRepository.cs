using System;
using System.Threading.Tasks;
using API.Core.Domain.Models;

namespace API.Core.Interfaces
{
    public interface ILikeRepository : IRepository<Likes>
    {
        Task<int> UpdateLike(Guid userId, string problemBetaId);
    }
}