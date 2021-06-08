using MediatR;
using SimpleShop.Application.Contracts.Persistence;
using SimpleShop.Application.Contracts.Persistence.Repositories;
using SimpleShop.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleShop.Application.Commands
{
    class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Product(
                request.Name,
                request.Description,
                request.Price,
                request.UnitsInStock);

            this.productRepository.Add(newProduct);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            return newProduct.Id;
        }
    }
}
