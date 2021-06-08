using MediatR;
using SimpleShop.Application.Contracts.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleShop.Application.Queries
{
    class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var p = await this.productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (p == null)
                return null;
            return ProductDto.FromProduct(p);
        }

    }
}
