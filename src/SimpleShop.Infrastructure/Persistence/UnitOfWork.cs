using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleShop.Application.Common;
using SimpleShop.Application.Contracts.Persistence;
using SimpleShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleShop.Infrastructure.Persistence
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private readonly IMediator mediator;

        public UnitOfWork(AppDbContext dbContext, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await DispatchDomainEvents(cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task DispatchDomainEvents(CancellationToken cancellationToken)
        {
            var domainEntities = this.dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent), cancellationToken);
            }
        }

        private static INotification GetNotificationCorrespondingToDomainEvent(IDomainEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}
