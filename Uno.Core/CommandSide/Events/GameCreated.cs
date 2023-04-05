namespace Uno.Core.CommandSide.Events;

public record GameCreated(GameId Id) : IDomainEvent
{
    public IAggregateId GetAggregateId() => Id;
}