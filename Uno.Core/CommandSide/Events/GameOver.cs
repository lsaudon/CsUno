namespace Uno.Core.CommandSide.Events
{
    public record GameOver : IDomainEvent
    {
        public GameId Id { get; }
        public string Winner { get; }


        public GameOver(GameId id, string winner)
        {
            Id = id;
            Winner = winner;
        }

        public IAggregateId GetAggregateId() => Id;
    }
}