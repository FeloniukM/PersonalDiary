using Microsoft.EntityFrameworkCore;
using PersonalDiary.DAL.Interfaces;
using System.Linq.Expressions;

namespace PersonalDiary.DAL.DataAccess
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly PersonalDiaryDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(PersonalDiaryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByKeyAsync<TKey>(TKey key)
        {
            var entity = await _dbSet.FindAsync(key);

            if (entity == null)
                throw new Exception();

            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;

            return Task.CompletedTask;
        }

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = includes
                .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(_dbSet, (current, include) => current.Include(include));

            return query;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
