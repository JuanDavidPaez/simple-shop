using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleShop.API.Core;
using SimpleShop.Application.Commands;
using SimpleShop.Application.Queries;
using SimpleShop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleShop.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly ILogger<ProductsController> logger;
        private readonly IMediator mediator;

        public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var products = await mediator.Send(new GetAllProductsQuery(), ct);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            if (id <= 0)
                return BadRequest("Request id should be greater than 0");
            var product = await mediator.Send(new GetProductByIdQuery(id), ct);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
            return await mediator.Send(command);
        }
    }
}
