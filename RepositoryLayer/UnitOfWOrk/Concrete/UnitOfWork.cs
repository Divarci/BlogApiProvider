using Microsoft.EntityFrameworkCore.Storage;
using RepositoryLayer.Context;
using RepositoryLayer.Repository.Abstract;
using RepositoryLayer.Repository.Concrete;
using RepositoryLayer.UnitOfWOrk.Abstract;

namespace RepositoryLayer.UnitOfWOrk.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }


        //savechange
        public void Commit()
        {
            _context.SaveChanges();
        }
        // async savechange
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }


        //dispose after commit
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }


        //transaction method
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }



        //brings all repositories via unitofworks
        IGenericRepository<T> IUnitOfWork.GetGenericRepository<T>()
        {
            return new GenericRepository<T>(_context);
        }

    }
}
