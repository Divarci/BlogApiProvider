using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Exceptions.Filters;
using ServiceLayer.FluentValidaton.Blog.Category;
using System.Reflection;

namespace ServiceLayer.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtensions(this IServiceCollection services)
        {
            //Add Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Add fluent validations ad options
            services.AddFluentValidationAutoValidation(opt =>
            {
                opt.DisableDataAnnotationsValidation = true;
            });
            services.AddValidatorsFromAssemblyContaining<CategoryAddDtoValidation>();

            //Add Services
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

            foreach (var type in types)
            {
                var serviceType = type.GetInterfaces().FirstOrDefault(i => i.Name == $"I{type.Name}");
                if (serviceType != null)
                {
                    services.AddScoped(serviceType, type);
                }
            }

            //Add MemoryCache
            services.AddMemoryCache();

            //We suppressed the original filter Dto as we have CustomResponseDto
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped(typeof(GenericNotFoundFilter<>));

            return services;
        }
    }
}
