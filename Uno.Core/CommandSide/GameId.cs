namespace Uno.Core.CommandSide;

public record GameId : IAggregateId
{
    public string Value { get; }

    public GameId(string value)
    {
        Value = value;
    }
}