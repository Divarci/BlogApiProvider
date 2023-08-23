using CoreLayer.BaseEntity;
using System.Linq.Expressions;

namespace RepositoryLayer.Repository.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        IQueryable<T> GetList();
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        void Delete(T entity);
        Task<T> GetByIdAsync(int id);
        void Update(T entity);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}
