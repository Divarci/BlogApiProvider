using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RepositoryLayer.Context;
using RepositoryLayer.Repository.Abstract;
using RepositoryLayer.Repository.Concrete;
using RepositoryLayer.UnitOfWOrk.Abstract;
using ServiceLayer.Exceptions.Exceptions;

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
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException)
                    throw new DbUpdateConcurrencyException("Data has been changed.");

                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                    throw new ConflictException("Please delete all articles related to this category!");
            }

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
