using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleShop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleShop.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // This strategy must be reviewed according to the specific needs and deployment process of the application
            await UpdateDatabaseMigrations(host);

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        static async Task UpdateDatabaseMigrations(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var migrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
                migrator.Migrate();
                await migrator.SeedAsync();
            }
        }
    }
}
