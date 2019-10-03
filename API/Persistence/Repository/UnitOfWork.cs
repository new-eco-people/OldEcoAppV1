using System.Threading.Tasks;
using API.Core.Domain.Models;
using API.Core.Interfaces;
using API.Helper.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IProjectRepository Projects { get; private set; }
        public IUserRepository Users { get; private set; }

        public IAuthRepository Auth { get; private set; }

        public IProblemBetaRepository ProbBeta { get; private set; }
        public ILikeRepository Likes { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IIdeaRepository Ideas { get; private set; }

        public ILocationRepository Location { get; set; }

        public UnitOfWork(DataContext context, UserManager<User> userManager, IHostingEnvironment env)
        {
            _context = context;
            Projects = new ProjectRepository(_context);
            Users = new UserRepository(_context, userManager);
            ProbBeta = new ProblemBetaRepository(_context, userManager, env, Users);
            Auth = new AuthRepository(userManager);
            Likes = new LikeRepository(_context);
            Comments = new CommentRepository(_context);
            Ideas = new IdeaRepository(_context);
            Location = new LocationRepository(_context);
        }

        // public void Add<T>(T entity) where T : class
        // {
        //     _context.Add(entity);
        // }

        // public void Update<T>(T entity) where T : class
        // {
        //     _context.Update(entity);
        // }

        // public void Delete<T>(T entity) where T : class
        // {
        //     _context.Remove(entity);
        // }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}