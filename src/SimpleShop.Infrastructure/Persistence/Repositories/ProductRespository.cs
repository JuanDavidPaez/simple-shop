using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Contracts.Persistence.Repositories;
using SimpleShop.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleShop.Infrastructure.Persistence.Repositories
{
    class ProductRespository : IProductRepository
    {
        private readonly AppDbContext dbContext;

        public ProductRespository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<Product>> GetAllAsync(CancellationToken ct = default)
        {
            return dbContext.Products.ToListAsync(ct);
        }

        public Task<Product> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return dbContext.Products.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public void Add(Product product)
        {
            dbContext.Products.AddAsync(product);
        }
    }
}
