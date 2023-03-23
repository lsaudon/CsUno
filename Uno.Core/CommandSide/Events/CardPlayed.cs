namespace Uno.Core.CommandSide.Events
{
    public record CardPlayed : IDomainEvent
    {
        public PlayerId Id { get; }
        public Card Card { get; }

        public CardPlayed(PlayerId id, Card card)
        {
            Id = id;
            Card = card;
        }

        public IAggregateId GetAggregateId() => Id;
    }
}