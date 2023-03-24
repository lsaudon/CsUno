using System.Collections.Generic;
using System.Linq;
using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Commands;
using Uno.Core.CommandSide.Events;
using Xunit;

namespace Uno.Core.Tests.CommandSide;

public class GameTests
{
    [Fact]
    public void When_Send_CreateGame_Then_Raise_GameCreated()
    {
        var domainEvents = Game.Decide(new CreateGame(new GameId("1")),
            new List<IDomainEvent>());

        Assert.Contains(domainEvents, e => e is GameCreated);
    }

    [Fact]
    public void Given_GameCreated_When_Send_CreateGame_Then_Raise_Nothing()
    {
        var events = new List<IDomainEvent> { new GameCreated(new GameId("1")) };

        var domainEvents = Game.Decide(new CreateGame(new GameId("1")), events);

        Assert.Empty(domainEvents);
    }

    [Fact]
    public void When_Send_JoinGame_Then_Raise_Nothing()
    {
        var events = new List<IDomainEvent>();

        var domainEvents = Game.Decide(new JoinGame(new GameId("1"), new PlayerId("1")), events);

        Assert.Empty(domainEvents);
    }

    [Fact]
    public void Given_GameCreated_When_Send_JoinGame_Then_Raise_PlayerJoined()
    {
        var events = new List<IDomainEvent> { new GameCreated(new GameId("1")) };

        var domainEvents = Game.Decide(new JoinGame(new GameId("1"), new PlayerId("1")), events);

        Assert.Contains(domainEvents, e => e is PlayerJoined);
    }


    [Fact]
    public void Given_PlayerJoined_When_Send_JoinGame_Same_Player_Then_Raise_Nothing()
    {
        var events = new List<IDomainEvent> {
            new GameCreated(new GameId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("1")),
        };

        var domainEvents = Game.Decide(new JoinGame(new GameId("1"), new PlayerId("1")), events);

        Assert.Empty(domainEvents);
    }

    [Fact]
    public void Given_PlayerJoined_10_Times_When_Send_JoinGame_Then_Raise_Nothing()
    {
        var events = new List<IDomainEvent> {
            new GameCreated(new GameId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("2")),
            new PlayerJoined(new GameId("1"), new PlayerId("3")),
            new PlayerJoined(new GameId("1"), new PlayerId("4")),
            new PlayerJoined(new GameId("1"), new PlayerId("5")),
            new PlayerJoined(new GameId("1"), new PlayerId("6")),
            new PlayerJoined(new GameId("1"), new PlayerId("7")),
            new PlayerJoined(new GameId("1"), new PlayerId("8")),
            new PlayerJoined(new GameId("1"), new PlayerId("9")),
            new PlayerJoined(new GameId("1"), new PlayerId("10")),
        };

        var domainEvents = Game.Decide(new JoinGame(new GameId("1"), new PlayerId("11")), events);

        Assert.Empty(domainEvents);
    }

    [Fact]
    public void Given_PlayerJoined_3_Times_When_Send_StartGame_Then_Raise_GameStarted()
    {
        var events = new List<IDomainEvent> {
            new GameCreated(new GameId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("2")),
            new PlayerJoined(new GameId("1"), new PlayerId("3")),
        };

        var domainEvents = Game.Decide(new StartGame(new GameId("1")), events);

        Assert.Contains(domainEvents, e => e is GameStarted);
        Assert.Equal(3, domainEvents.Count(e => e is SevenCardsDealt));
    }

    [Fact]
    public void Given_PlayerJoined_2_Times_When_Send_StartGame_Then_Raise_Nothing()
    {
        var events = new List<IDomainEvent> {
            new GameCreated(new GameId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("2")),
        };

        var domainEvents = Game.Decide(new StartGame(new GameId("1")), events);

        Assert.Empty(domainEvents);
    }

    [Fact]
    public void Given_GameStarted_When_Send_JoinGame_Then_Raise_Nothing()
    {
        var events = new List<IDomainEvent> {
            new GameCreated(new GameId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("2")),
            new PlayerJoined(new GameId("1"), new PlayerId("3")),
            new GameStarted(new GameId("1")),
        };

        var domainEvents = Game.Decide(new JoinGame(new GameId("1"), new PlayerId("4")), events);

        Assert.Empty(domainEvents);
    }
}