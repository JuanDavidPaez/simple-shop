using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SimpleShop.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SimpleShop.Infrastructure.Persistence
{
    class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

    // Factory used to create the dbContext when managing migrations locally
    internal class DesignTimeContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer($@"Server=(localdb)\MSSQLLocalDB;Trusted_Connection=True;DataBase=DbDesign_SimpleShop_{nameof(AppDbContext)};");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
