namespace Uno.Core.CommandSide.Events
{
    public record GameCreated : IDomainEvent
    {
        public GameId Id { get; }
        public string Name { get; }


        public GameCreated(GameId id, string name)
        {
            Id = id;
            Name = name;
        }

        public IAggregateId GetAggregateId() => Id;
    }
}