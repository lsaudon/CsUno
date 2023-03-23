namespace Uno.Core.CommandSide.Commands
{
    public record CreateGame(GameId Id, string Name) : IDomainCommand;
}