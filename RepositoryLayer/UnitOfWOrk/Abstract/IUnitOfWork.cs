using CoreLayer;
using RepositoryLayer.Repository.Abstract;

namespace RepositoryLayer.UnitOfWOrk.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<T> GetGenericRepository<T>() where T : BaseEntity, new();
    }
}
