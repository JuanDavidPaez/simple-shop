using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Domain.Common
{

    public interface IDomainEvent
    {
        Guid Id { get; }
        DateTimeOffset TimeStamp { get; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        public DateTimeOffset TimeStamp { get; private set; }

        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            TimeStamp = DateTimeOffset.UtcNow;
        }

    }
}
