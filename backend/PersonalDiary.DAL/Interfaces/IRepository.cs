using System.Linq.Expressions;

namespace PersonalDiary.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByKeyAsync<TKey>(TKey key);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
        Task<int> SaveChangesAsync();
    }
}
