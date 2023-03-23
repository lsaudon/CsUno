namespace Uno.Core.CommandSide.Commands
{
    public record PlayCard(GameId GameId, string PlayerName, CardColor Color, CardValue Value) : IDomainCommand;
}