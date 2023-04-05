using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newsletter.Infrastructure.Persistence;
using Newsletter.Infrastructure.Repositories;
using System.Reflection;

namespace Newsletter.Api.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            var assemblies = new Assembly[]{
                typeof(TitelRepository).Assembly
            };

            foreach (var implementationType in assemblies.SelectMany(x => x.GetTypes())
                         .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Handler") || t.Name.EndsWith("Query")))
            {
                foreach (var interfaceType in implementationType.GetInterfaces())
                {
                    services.AddScoped(interfaceType, implementationType);
                }
            }

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NewsletterDatastore>(options =>
                options.UseSqlServer(connectionString)
            );

            return services;
        }
    }
}
