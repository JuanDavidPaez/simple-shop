using MediatR;
using SimpleShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Application.Common
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }

        public TDomainEvent DomainEvent { get; }
    }
}
