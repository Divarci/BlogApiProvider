using CoreLayer;
using System.Linq.Expressions;

namespace RepositoryLayer.Repository.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        Task Add(T entity);
        IQueryable<T> GetList();
        Task<bool> AnyAsync();
        void Delete(T entity);
        Task<T> GetById(int id);
        void Update(T entity);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}
