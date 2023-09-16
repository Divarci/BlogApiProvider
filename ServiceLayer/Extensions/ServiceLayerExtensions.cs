using EntityLayer.Token.DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Exceptions.Filters;
using ServiceLayer.FluentValidaton.Blog.Category;
using ServiceLayer.Helpers.Image;
using ServiceLayer.Helpers.SignHelpers;
using System.Reflection;

namespace ServiceLayer.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtensions(this IServiceCollection services,IConfiguration configuration)
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

            services.AddScoped<IImageHelper,ImageHelper>();

            //Add MemoryCache
            services.AddMemoryCache();

            //We suppressed the original filter Dto as we have CustomResponseDto
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddScoped(typeof(GenericNotFoundFilter<>));


            //services.Configure<TokenInfo>(configuration.GetSection("TokenOptions"));


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var tokenInfo = configuration.GetSection("TokenOptions").Get<TokenInfo>()!;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = tokenInfo.Issuer,
                    ValidAudience = tokenInfo.Audience[0],
                    IssuerSigningKey = SignHelper.GetSymmetricSecurityKey(tokenInfo.SecurityKey),

                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero,

                };

                
            });




            return services;
        }
    }
}
