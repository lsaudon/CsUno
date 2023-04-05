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
    public void Given_PlayerJoined_3_Times_When_Send_StartGame_Then_Raise_GameStarted_3_CardsDealt_And_PileOfCardsMade()
    {
        var events = new List<IDomainEvent> {
            new GameCreated(new GameId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("1")),
            new PlayerJoined(new GameId("1"), new PlayerId("2")),
            new PlayerJoined(new GameId("1"), new PlayerId("3")),
        };

        var domainEvents = Game.Decide(new StartGame(new GameId("1")), events).ToList();

        Assert.Contains(domainEvents, e => e is GameStarted);
        var cardsDealts = domainEvents.OfType<CardsDealt>().ToList();
        Assert.NotNull(cardsDealts);
        Assert.Equal(3, cardsDealts.Count);
        Assert.All(cardsDealts, a => Assert.Equal(7, a.Cards.Count));
        var pileOfCardsMade = domainEvents.OfType<PileOfCardsMade>().First();
        Assert.NotNull(pileOfCardsMade);
        Assert.Equal(55, pileOfCardsMade.Cards.Count);
        var cards = cardsDealts.SelectMany(a => a.Cards).Concat(pileOfCardsMade.Cards).ToList();
        foreach (var card in cards)
        {
            if (card.Number.Value == 0)
            {
                Assert.Single(cards.Where(a => a.Equals(card)));
            }
            else
            {
                Assert.Equal(2, cards.Where(a => a.Equals(card)).Count());
            }
        }
        Assert.Equal(76, cards.Count);
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