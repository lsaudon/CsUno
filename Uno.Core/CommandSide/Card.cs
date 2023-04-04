namespace Uno.Core.CommandSide;

public enum CardColor
{
    Red,
    Yellow,
    Green,
    Blue
}

public record Card(CardColor Color, int Number) { }