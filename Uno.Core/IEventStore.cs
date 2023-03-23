using System.Collections.Generic;
using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Events;

namespace Uno.Core
{
    public interface IEventStore
    {
        IReadOnlyCollection<IDomainEvent> GetAll(IAggregateId aggregateId);
        void Clear(IAggregateId aggregateId);
        void Add(IDomainEvent domainEvent, int sequenceId);
    }
}