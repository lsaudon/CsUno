namespace Uno.Core.CommandSide.Commands;

public record JoinGame(GameId GameId, PlayerId PlayerId) : IDomainCommand;