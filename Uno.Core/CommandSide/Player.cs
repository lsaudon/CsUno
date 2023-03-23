using System.Collections.Generic;

namespace Uno.Core.CommandSide
{
    public record Player(string Name, IList<Card> Hand);
}