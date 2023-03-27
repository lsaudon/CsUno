using System.Collections.Generic;
using System.Linq;
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
        return decisionProjection.NumberOfPlayers < 3
            ? new List<IDomainEvent>()
            : new List<IDomainEvent> { new GameStarted(command.Id) }
                                     .Concat(decisionProjection.PlayerIds.Select(e => new SevenCardsDealt(command.Id, e)));
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
            AddHandler<SevenCardsDealt>(When);
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

        private void When(SevenCardsDealt _)
        {
        }
    }
}
