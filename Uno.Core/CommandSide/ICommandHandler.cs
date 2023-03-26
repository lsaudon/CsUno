﻿using Uno.Core.CommandSide.Commands;

namespace Uno.Core.CommandSide;

public interface ICommandHandler<in TCommand> where TCommand : notnull, IDomainCommand
{
    void Handle(TCommand command);
}
