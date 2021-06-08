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
    class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await this.productRepository.GetAllAsync(cancellationToken);
            var list = products.Select(x => ProductDto.FromProduct(x));
            return list;
        }
    }
}
