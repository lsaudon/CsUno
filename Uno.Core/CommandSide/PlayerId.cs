namespace Uno.Core.CommandSide
{
    public record PlayerId : IAggregateId
    {
        public string Value { get; }

        public PlayerId(string value)
        {
            Value = value;
        }
    }
}