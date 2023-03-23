namespace Uno.Core.CommandSide.Events
{
    public record CardDrawn(int NumCards) : IDomainEvent
    {
        public IAggregateId GetAggregateId()
        {
            throw new System.NotImplementedException();
        }
    }
}