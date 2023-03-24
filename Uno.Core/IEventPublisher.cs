using Uno.Core.CommandSide.Events;

namespace Uno.Core;

public interface IEventPublisher
{
    void Publish<TEvent>(TEvent evt, int sequenceId) where TEvent : notnull, IDomainEvent;
}
