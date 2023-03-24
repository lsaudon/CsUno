namespace Uno.Core.CommandSide.Commands;

public record StartGame(GameId Id) : IDomainCommand;
