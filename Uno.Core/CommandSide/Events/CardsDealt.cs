using System.Collections.Generic;

namespace Uno.Core.CommandSide.Events;

public record CardsDealt(GameId Id, PlayerId PlayerId, IList<Card> Cards) : IDomainEvent
{
    public IAggregateId GetAggregateId() => Id;
}