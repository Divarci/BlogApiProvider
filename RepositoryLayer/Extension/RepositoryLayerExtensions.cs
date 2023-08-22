using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;
using RepositoryLayer.Repository.Abstract;
using RepositoryLayer.Repository.Concrete;
using RepositoryLayer.UnitOfWOrk.Abstract;
using RepositoryLayer.UnitOfWOrk.Concrete;

namespace RepositoryLayer.Extension
{
    public static class RepositoryLayerExtensions
    {
        public static IServiceCollection LoadRepositoryLayerExtensions(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("SqlConnection")));

            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            

            return services;
        }
    }
}
