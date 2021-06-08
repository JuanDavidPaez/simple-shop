using SimpleShop.Domain.Contracts;
using SimpleShop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Domain.Common
{
    public abstract class Entity
    {
        private List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            this._domainEvents.Add(domainEvent);
        }

        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule.Message);
            }
        }

        protected void CheckRule(Func<bool> rule, string message)
        {
            if (rule())
            {
                throw new BusinessRuleValidationException(message);
            }
        }
    }
}
