namespace Uno.Core.CommandSide.Events;

public class SevenCardsDealt : IDomainEvent
{
    public GameId Id { get; }
    public PlayerId PlayerId { get; }

    public SevenCardsDealt(GameId id, PlayerId playerId)
    {
        Id = id;
        PlayerId = playerId;
    }

    public IAggregateId GetAggregateId() => Id;
}