using System.Collections.Generic;

namespace Uno.Core.CommandSide.Events;
public record PileOfCardsMade : IDomainEvent
{
    public GameId Id { get; }
    public IList<Card> Cards { get; }

    public PileOfCardsMade(GameId id, IList<Card> cards)
    {
        Id = id;
        Cards = cards;
    }

    public IAggregateId GetAggregateId() => Id;
}