namespace Uno.Core.CommandSide.Events
{
    public record PlayerJoined : IDomainEvent
    {
        public PlayerId Id { get; }

        public string Name { get; }

        public PlayerJoined(PlayerId id, string name)
        {
            Id = id;
            Name = name;
        }

        public IAggregateId GetAggregateId() => Id;
    }
}