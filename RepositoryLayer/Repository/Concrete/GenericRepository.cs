using CoreLayer;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Repository.Abstract;
using System.Linq.Expressions;

namespace RepositoryLayer.Repository.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        //Dependancy Injections
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbset;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }



        //All Generic Methods
        public async Task Add(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public IQueryable<T> GetList()
        {
            return _dbset.AsNoTracking().AsQueryable();
        }

        public async Task<bool> AnyAsync()
        {
            return await _dbset.AnyAsync();
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public async Task<T> GetById(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression);
        }


    }
}
