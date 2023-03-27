using System.Collections.Generic;
using System.Linq;
using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Commands;
using Uno.Core.QuerySide;
using Uno.Infrastructure;
using Uno.Infrastructure.EventStore;
using Xunit;

namespace Uno.Integration.Tests;

public class ProjectionTests
{
    [Fact]
    public void When_Send_CreateGame_Then_Display_Updated_Projection()
    {
        var repository = new List<CreatingGame>();
        var pendingOrderEventHandler = new CreatingGameEventHandler(repository);
        var eventHandlers = new List<IEventHandler> { pendingOrderEventHandler };

        var eventStore = new EventStoreInMemory();
        var eventPublisher = new EventPublisher(eventStore, eventHandlers);
        var commandHandler = new CommandHandler(eventPublisher, eventStore);

        commandHandler.Handle(new CreateGame(new GameId("1")));

        Assert.Single(repository);
        Assert.Equal(new GameId("1"), repository.First().Id);
    }

    [Fact]
    public void When_Send_CreateGame_Two_Times_With_Different_Id_Then_Display_Updated_Projection()
    {
        var repository = new List<CreatingGame>();
        var pendingOrderEventHandler = new CreatingGameEventHandler(repository);
        var eventHandlers = new List<IEventHandler> { pendingOrderEventHandler };

        var eventStore = new EventStoreInMemory();
        var eventPublisher = new EventPublisher(eventStore, eventHandlers);
        var commandHandler = new CommandHandler(eventPublisher, eventStore);

        commandHandler.Handle(new CreateGame(new GameId("1")));
        commandHandler.Handle(new CreateGame(new GameId("2")));

        Assert.Equal(2, repository.Count);
        Assert.Equal(new GameId("1"), repository[0].Id);
        Assert.Equal(new GameId("2"), repository[1].Id);
    }

    [Fact]
    public void When_CreateGame_Three_Players_JoinGame_And_StartGame_Then_Display_Updated_Projection()
    {
        var repository = new List<CreatingGame>();
        var pendingOrderEventHandler = new CreatingGameEventHandler(repository);
        var dictionary = new Dictionary<GameId, int>();
        var numberOfPlayersInGameEventHandler = new NumberOfPlayersInGameEventHandler(dictionary);
        var eventHandlers = new List<IEventHandler> { pendingOrderEventHandler,
                                                      numberOfPlayersInGameEventHandler };

        var eventStore = new EventStoreInMemory();
        var eventPublisher = new EventPublisher(eventStore, eventHandlers);
        var commandHandler = new CommandHandler(eventPublisher, eventStore);

        commandHandler.Handle(new CreateGame(new GameId("1")));
        commandHandler.Handle(new JoinGame(new GameId("1"), new PlayerId("1")));
        commandHandler.Handle(new JoinGame(new GameId("1"), new PlayerId("2")));
        commandHandler.Handle(new JoinGame(new GameId("1"), new PlayerId("3")));
        commandHandler.Handle(new StartGame(new GameId("1")));

        Assert.Empty(repository);
        Assert.Equal(3, dictionary[new GameId("1")]);
    }
}