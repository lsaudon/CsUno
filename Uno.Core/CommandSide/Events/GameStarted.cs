namespace Uno.Core.CommandSide.Events;

public record GameStarted(GameId Id) : IDomainEvent
{
    public IAggregateId GetAggregateId() => Id;
}