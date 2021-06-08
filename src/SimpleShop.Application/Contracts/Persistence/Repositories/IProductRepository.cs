using SimpleShop.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleShop.Application.Contracts.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(CancellationToken ct = default);
        Task<Product> GetByIdAsync(int id, CancellationToken ct = default);
        void Add(Product product);
    }
}
