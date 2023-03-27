namespace Uno.Core.CommandSide.Commands;

public record JoinGame(GameId Id, PlayerId PlayerId) : IDomainCommand;