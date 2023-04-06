using System.Collections.Generic;
using Uno.Core.CommandSide;

namespace Uno.Core.QuerySide;

public record PlayerCards(GameId Id, PlayerId PlayerId, IList<Card> Cards);
