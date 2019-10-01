using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _context;

        public Repository(DataContext context)
        {
            this._context = context;
        }
        public async Task Add(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
           await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
           return _context.Set<TEntity>().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetEntity(string Id)
        {
            // This is used because Guid is being used
            Guid value;
            if(!Guid.TryParse(Id, out value))
                value = Guid.Empty;

            return await _context.Set<TEntity>().FindAsync(value);
        }

        public async Task<TEntity> GetEntityBy(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<int> TotalItems()
        {
            return await _context.Set<TEntity>().CountAsync();
        }
    }
}