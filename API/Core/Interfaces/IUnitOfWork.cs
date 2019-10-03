using System;
using System.Threading.Tasks;

namespace API.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // void Add<T>(T entity) where T:class;
        // void Delete<T>(T entity) where T:class;
        // void Update<T>(T entity) where T:class;
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
        IAuthRepository Auth {get; }
        IProblemBetaRepository ProbBeta {get; }
        ILikeRepository Likes {get; }
        ICommentRepository Comments {get; }
        IIdeaRepository Ideas {get; }
        ILocationRepository Location { get; set; }
        Task<bool> Complete();
    }
}