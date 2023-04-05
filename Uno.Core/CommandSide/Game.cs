using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Uno.Core.CommandSide.Commands;
using Uno.Core.CommandSide.Events;

namespace Uno.Core.CommandSide;

public static class Game
{
    public static IEnumerable<IDomainEvent> Decide(IDomainCommand command, IReadOnlyCollection<IDomainEvent> pastEvents)
    {
        var decisionProjection = new DecisionProjection();

        foreach (var domainEvent in pastEvents)
        {
            decisionProjection.Apply(domainEvent);
        }

        return command switch
        {
            CreateGame c => Decide(c, decisionProjection),
            JoinGame c => Decide(c, decisionProjection),
            StartGame c => Decide(c, decisionProjection),
            _ => new List<IDomainEvent>()
        };
    }

    private static IEnumerable<IDomainEvent> Decide(CreateGame command, DecisionProjection decisionProjection)
    {
        return decisionProjection.IsCreated
            ? new List<IDomainEvent>()
            : new List<IDomainEvent> { new GameCreated(command.Id) };
    }

    private static IEnumerable<IDomainEvent> Decide(JoinGame command, DecisionProjection decisionProjection)
    {
        return !decisionProjection.IsCreated ||
               decisionProjection.NumberOfPlayers >= 10 ||
               decisionProjection.PlayerIds.Contains(command.PlayerId) ||
               decisionProjection.IsStarted
            ? new List<IDomainEvent>()
            : new List<IDomainEvent> { new PlayerJoined(command.Id, command.PlayerId) };
    }

    private static IEnumerable<IDomainEvent> Decide(StartGame command, DecisionProjection decisionProjection)
    {
        if (decisionProjection.NumberOfPlayers < 3)
        {
            return new List<IDomainEvent>();
        }

        var cards = ShuffleAllCards().ToList();

        var cardsDealts = decisionProjection.PlayerIds.Select(e =>
        {
            var playerCards = cards.Take(7).ToList();
            cards.RemoveRange(0, playerCards.Count);
            return new CardsDealt(command.Id, e, playerCards);
        }).ToList();

        var list = new List<IDomainEvent> { new GameStarted(command.Id) }.Concat(cardsDealts).ToList();
        list.Add(new PileOfCardsMade(command.Id, cards));
        return list;
    }

    private static IEnumerable<Card> ShuffleAllCards()
    {
        List<Card> cards = new();
        for (int i = 0; i < 10; i++)
        {
            CardNumber number = new(i);
            cards.Add(new Card(CardColor.Red, number));
            cards.Add(new Card(CardColor.Yellow, number));
            cards.Add(new Card(CardColor.Green, number));
            cards.Add(new Card(CardColor.Blue, number));
            if (i > 0)
            {
                cards.Add(new Card(CardColor.Red, number));
                cards.Add(new Card(CardColor.Yellow, number));
                cards.Add(new Card(CardColor.Green, number));
                cards.Add(new Card(CardColor.Blue, number));
            }
        }
        return cards.OrderBy(_ => RandomNumberGenerator.GetInt32(cards.Count));
    }

    private sealed class DecisionProjection : DecisionProjectionBase
    {
        public bool IsCreated { get; private set; }
        public bool IsStarted { get; private set; }
        public List<PlayerId> PlayerIds { get; } = new List<PlayerId>();
        public int NumberOfPlayers => PlayerIds.Count;

        public DecisionProjection()
        {
            AddHandler<GameCreated>(When);
            AddHandler<PlayerJoined>(When);
            AddHandler<GameStarted>(When);
        }


        private void When(GameCreated _)
        {
            IsCreated = true;
        }

        private void When(PlayerJoined evt)
        {
            PlayerIds.Add(evt.PlayerId);
        }

        private void When(GameStarted _)
        {
            IsStarted = true;
        }
    }
}
