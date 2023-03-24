namespace Uno.Core.CommandSide.Events;

public interface IDomainEvent
{
    IAggregateId GetAggregateId();
}
