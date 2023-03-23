namespace Uno.Core.CommandSide.Commands
{
    public record JoinGame(GameId GameId, string PlayerName) : IDomainCommand;
}