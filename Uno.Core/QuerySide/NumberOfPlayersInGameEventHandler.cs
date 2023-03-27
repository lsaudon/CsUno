using System.Collections.Generic;
using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Events;

namespace Uno.Core.QuerySide;

public class NumberOfPlayersInGameEventHandler : IEventHandler
{
    private readonly Dictionary<GameId, int> dictionary;

    public NumberOfPlayersInGameEventHandler(Dictionary<GameId, int> dictionary)
    {
        this.dictionary = dictionary;
    }

    public void Handle(IDomainEvent evt)
    {
        if (evt is PlayerJoined playerJoined)
        {
            if (dictionary.ContainsKey(playerJoined.Id))
            {
                dictionary[playerJoined.Id] += 1;
                return;
            }
            dictionary.Add(playerJoined.Id, 1);
        }
    }
}
