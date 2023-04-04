using System.Collections.Generic;

namespace Uno.Core.CommandSide.Events;

public record CardsDealt : IDomainEvent
{
    public GameId Id { get; }
    public PlayerId PlayerId { get; }
    public IList<Card> Cards { get; }

    public CardsDealt(GameId id, PlayerId playerId, IList<Card> cards)
    {
        Id = id;
        PlayerId = playerId;
        Cards = cards;
    }

    public IAggregateId GetAggregateId() => Id;
}