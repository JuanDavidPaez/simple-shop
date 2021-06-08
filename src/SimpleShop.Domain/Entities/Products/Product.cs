using SimpleShop.Domain.Common;
using SimpleShop.Domain.Entities.Products.Events;
using SimpleShop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Domain.Entities.Products
{
    public class Product : Entity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int UnitsInStock { get; private set; }

        // Used by EF only
        private Product() { }

        public Product(string name, string description, decimal price, int unitsInStock)
        {
            CheckRule(() => string.IsNullOrWhiteSpace(name), "Name must not be null or empty");
            CheckRule(() => string.IsNullOrWhiteSpace(description), "Description must not be null or empty");

            Id = 0;
            Name = name;
            Description = description;
            Price = price;
            UnitsInStock = unitsInStock;

            this.AddDomainEvent(new ProductCreatedDomainEvent(this));
        }

    }
}
