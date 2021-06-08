using SimpleShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Domain.Entities.Products.Events
{
    public class ProductCreatedDomainEvent : DomainEvent
    {
        public Product Product { get; }

        public ProductCreatedDomainEvent(Product product)
        {
            this.Product = product;
        }
    }
}
