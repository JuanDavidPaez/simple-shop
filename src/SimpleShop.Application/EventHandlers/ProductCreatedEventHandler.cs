using MediatR;
using SimpleShop.Application.Common;
using SimpleShop.Domain.Entities.Products.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleShop.Application.EventHandlers
{
    class ProductCreatedEventHandler : INotificationHandler<DomainEventNotification<ProductCreatedDomainEvent>>
    {
        public Task Handle(DomainEventNotification<ProductCreatedDomainEvent> notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
