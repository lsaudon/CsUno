namespace Uno.Core.CommandSide;

public enum CardColor
{
    Red,
    Yellow,
    Green,
    Blue
}

public record CardNumber(int Value);

public record Card(CardColor Color, CardNumber Number);