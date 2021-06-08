using SimpleShop.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Application.Queries
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }

        public static ProductDto FromProduct(Product p)
        {
            return new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                UnitsInStock = p.UnitsInStock
            };
        }
    }
}
