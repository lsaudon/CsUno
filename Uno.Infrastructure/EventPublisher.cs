using System.Collections.Generic;
using Uno.Core;
using Uno.Core.CommandSide.Events;
using Uno.Core.QuerySide;

namespace Uno.Infrastructure;

public class EventPublisher : IEventPublisher
{
    private readonly IEventStore _eventStore;
    private readonly IReadOnlyCollection<IEventHandler> _eventHandlers;

    public EventPublisher(IEventStore eventStore, IReadOnlyCollection<IEventHandler> eventHandlers)
    {
        _eventStore = eventStore;
        _eventHandlers = eventHandlers;
    }

    public void Publish<TEvent>(TEvent evt, int sequenceId) where TEvent : IDomainEvent
    {
        _eventStore.Add(evt, sequenceId);
        foreach (var eventHandler in _eventHandlers)
        {
            eventHandler.Handle(evt);
        }
    }
}
