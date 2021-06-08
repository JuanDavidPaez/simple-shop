using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleShop.Application.Contracts.Persistence;
using SimpleShop.Application.Contracts.Persistence.Repositories;
using SimpleShop.Infrastructure.Persistence;
using SimpleShop.Infrastructure.Persistence.Repositories;
using System;

namespace SimpleShop.Infrastructure
{
    public static class ModuleDependencyInjection
    {
        public static void AddInfrastructureModule(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConnectionString));
            services.AddTransient<IDatabaseMigrator, EfDatabaseMigrator>();
            services.AddTransient<IProductRepository, ProductRespository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            /*Todo: review
            services.Scan(s =>
                s.FromAssemblyOf<UserRepository>()
                .AddClasses(x => x.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );*/
        }
    }
}
