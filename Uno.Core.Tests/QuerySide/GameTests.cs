using System.Collections.Generic;
using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Events;
using Uno.Core.QuerySide;
using Xunit;

namespace Uno.Core.Tests.QuerySide;

public class GameTests
{
    [Fact]
    public void When_Receive_GameCreated_Then_This_Game_Is_Added_In_Creating_Games()
    {
        var repository = new List<CreatingGame>();
        var eventHandler = new CreatingGameEventHandler(repository);

        eventHandler.Handle(new GameCreated(new GameId("1")));

        Assert.Single(repository);
    }

    [Fact]
    public void When_Receive_GameStarted_Then_This_Game_Is_Removed_Of_Creating_Games()
    {
        var repository = new List<CreatingGame> { new CreatingGame(new GameId("1")) };
        var eventHandler = new CreatingGameEventHandler(repository);

        eventHandler.Handle(new GameStarted(new GameId("1")));

        Assert.Empty(repository);
    }
}