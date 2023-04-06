using System.Collections.Generic;
using Uno.Core.CommandSide.Events;

namespace Uno.Core.QuerySide;
public class PlayerCardsEventHandler : IEventHandler
{
    private readonly List<PlayerCards> repository;

#pragma warning disable CA1002 // Do not expose generic lists
    public PlayerCardsEventHandler(List<PlayerCards> repository)
#pragma warning restore CA1002 // Do not expose generic lists
    {
        this.repository = repository;
    }

    public void Handle(IDomainEvent evt)
    {
        if (evt is CardsDealt cardsDealt)
        {
            PlayerCards playerCards = new(cardsDealt.Id, cardsDealt.PlayerId, cardsDealt.Cards);
            repository.Add(playerCards);
        }
    }
}
