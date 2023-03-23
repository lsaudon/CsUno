using System;
using System.Collections.Generic;

namespace Uno.Core.CommandSide
{
    public record Game(Guid Id, string Name, IList<Card> Deck, IList<Card> DiscardPile, IList<Player> Players, Player CurrentPlayer, bool IsGameOver);
}