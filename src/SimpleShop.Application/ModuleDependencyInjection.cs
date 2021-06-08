using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SimpleShop.Infrastructure
{
    public static class ModuleDependencyInjection
    {
        public static void AddApplicationModule(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
