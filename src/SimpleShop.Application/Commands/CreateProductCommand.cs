using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleShop.Application.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }

    }
}
