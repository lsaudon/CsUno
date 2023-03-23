namespace Uno.Core.CommandSide.Events
{
    public record PlayerTurnChanged : IDomainEvent
    {
        public PlayerId Id { get; }

        public string Name { get; }

        public PlayerTurnChanged(PlayerId id, string name)
        {
            Id = id;
            Name = name;
        }

        public IAggregateId GetAggregateId() => Id;
    }
}