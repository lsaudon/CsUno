using System.Collections.Generic;
using System.Linq;
using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Events;
using Uno.Core.QuerySide;
using Xunit;

namespace Uno.Core.Tests.QuerySide;

public class NumberOfPlayersInGameTests
{
    [Fact]
    public void When_Receive_PlayerJoined_Then_Added_In_Number_Of_Players_In_Game()
    {
        var dictionary = new Dictionary<GameId, int>();
        var eventHandler = new NumberOfPlayersInGameEventHandler(dictionary);

        eventHandler.Handle(new PlayerJoined(new GameId("1"), new PlayerId("1")));

        Assert.Single(dictionary);
        Assert.Equal(new GameId("1"), dictionary.First().Key);
        Assert.Equal(1, dictionary.First().Value);
    }

    [Fact]
    public void Given_One_Game_With_One_Player_When_Receive_PlayerJoined_Then_Two_Number_Of_Players_In_Game()
    {
        var repository = new Dictionary<GameId, int>() { { new GameId("1"), 1 } };
        var eventHandler = new NumberOfPlayersInGameEventHandler(repository);

        eventHandler.Handle(new PlayerJoined(new GameId("1"), new PlayerId("1")));

        Assert.Single(repository);
        Assert.Equal(new GameId("1"), repository.First().Key);
        Assert.Equal(2, repository.First().Value);
    }
}
