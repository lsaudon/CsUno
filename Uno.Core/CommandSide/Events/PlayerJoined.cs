namespace Uno.Core.CommandSide.Events;

public record PlayerJoined(GameId Id, PlayerId PlayerId) : IDomainEvent
{
    public IAggregateId GetAggregateId() => Id;
}