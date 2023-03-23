using System.Collections.Generic;
using Uno.Core.CommandSide.Commands;
using Uno.Core.CommandSide.Events;

namespace Uno.Core.CommandSide
{
    public static class GameProcessing
    {
        public static IEnumerable<IDomainEvent> Decide(IDomainCommand command, IEnumerable<IDomainEvent> pastEvents)
        {
            var decisionProjection = new DecisionProjection();
            foreach (var domainEvent in pastEvents)
            {
                decisionProjection.Apply(domainEvent);
            }

            var uncommittedEvents = command switch
            {
                CreateGame createGame => Decide(createGame, decisionProjection),
                _ => new List<IDomainEvent>()
            };

            return uncommittedEvents;
        }

        private static IEnumerable<IDomainEvent> Decide(CreateGame createGame, DecisionProjection decisionProjection)
        {
            return decisionProjection.IsStarted
                ? new List<IDomainEvent>()
                : new List<IDomainEvent> { new GameCreated(createGame.Id, createGame.Name) };
        }


        private sealed class DecisionProjection : DecisionProjectionBase
        {
            public bool IsStarted { get; private set; }
            public string Name { get; private set; } = string.Empty;

            public DecisionProjection()
            {
                AddHandler<GameCreated>(When);
            }

            private void When(GameCreated gameCreated)
            {
                IsStarted = true;
                Name = gameCreated.Name;
            }
        }
    }
}
