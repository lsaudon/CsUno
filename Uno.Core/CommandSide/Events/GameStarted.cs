namespace Uno.Core.CommandSide.Events;

public record GameStarted : IDomainEvent
{
    public GameId Id { get; }

    public GameStarted(GameId id)
    {
        Id = id;
    }

    public IAggregateId GetAggregateId() => Id;
}