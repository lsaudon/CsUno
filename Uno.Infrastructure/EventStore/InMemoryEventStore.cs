using System.Collections.Generic;
using Uno.Core.CommandSide.Events;
using Uno.Core.CommandSide;
using Uno.Core;

namespace Uno.Infrastructure.EventStore;

public class EventStoreInMemory : IEventStore
{
    private readonly Dictionary<IAggregateId, IList<IDomainEvent>> _dictionary = new();

    public void Add(IDomainEvent domainEvent, int sequenceId)
    {
        var aggregateId = domainEvent.GetAggregateId();
        if (_dictionary.TryGetValue(aggregateId, out var value))
        {
            var domainEvents = value;
            if (domainEvents.Count >= sequenceId)
            {
                throw new SequenceAlreadyStoredException();
            }

            domainEvents.Add(domainEvent);
            return;
        }

        _dictionary.Add(aggregateId, new List<IDomainEvent> { domainEvent });
    }

    public void Clear(IAggregateId aggregateId)
    {
        _dictionary.Remove(aggregateId);
    }

    public IReadOnlyCollection<IDomainEvent> GetAll(IAggregateId aggregateId)
    {
        return (_dictionary.TryGetValue(aggregateId, out var value) ? value : new List<IDomainEvent>()).AsReadOnly();
    }
}
