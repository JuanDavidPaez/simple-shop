using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }

    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; private set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
