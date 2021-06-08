using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Infrastructure.Persistence
{
    class EfDatabaseMigrator : IDatabaseMigrator
    {
        private readonly AppDbContext dbContext;

        public EfDatabaseMigrator(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Migrate()
        {
            dbContext.Database.Migrate();
        }

        public async Task SeedAsync()
        {
            if (!await dbContext.Products.AnyAsync())
            {
                await dbContext.Products.AddRangeAsync(GetProducts());
                await dbContext.SaveChangesAsync();
            }
        }

        private List<Product> GetProducts()
        {
            return new List<Product>() {
                new Product("Dell laptop", "8Gb Ram, 250 SDD, 14'' Screen, Core i7 8th Gen", 15.6m, 153)
            };
        }
    }
}
