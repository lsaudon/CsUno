using System.Collections.Generic;
using Uno.Core.CommandSide.Events;

namespace Uno.Core.QuerySide;

public class CreatingGameEventHandler : IEventHandler
{
    private readonly List<CreatingGame> repository;

#pragma warning disable CA1002 // Do not expose generic lists
    public CreatingGameEventHandler(List<CreatingGame> repository)
#pragma warning restore CA1002 // Do not expose generic lists
    {
        this.repository = repository;
    }

    public void Handle(IDomainEvent evt)
    {
        if (evt is GameCreated gameCreated)
        {
            repository.Add(new CreatingGame(gameCreated.Id));
            return;
        }
        if (evt is GameStarted gameStarted)
        {
            repository.RemoveAll(e => e.Id == gameStarted.Id);
        }
    }
}
