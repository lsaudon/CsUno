using System.Collections.Generic;

namespace Uno.Core.CommandSide.Events;
public record PileOfCardsMade(GameId Id, IList<Card> Cards) : IDomainEvent
{
    public IAggregateId GetAggregateId() => Id;
}