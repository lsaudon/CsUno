using System;
using Uno.Core.CommandSide.Commands;
using Uno.Core.CommandSide.Events;

namespace Uno.Core.CommandSide;

public class CommandHandler : ICommandHandler<CreateGame>,
                              ICommandHandler<JoinGame>
{
    private readonly IEventPublisher _eventPublisher;
    private readonly IEventStore _eventStoreInMemory;

    public CommandHandler(IEventPublisher eventPublisher, IEventStore eventStoreInMemory)
    {
        _eventPublisher = eventPublisher;
        _eventStoreInMemory = eventStoreInMemory;
    }

    public void Handle(CreateGame command)
    {
        var stream = _eventStoreInMemory.GetAll(command.Id);
        _eventPublisher.Publish(new GameCreated(command.Id), stream.Count);
    }

    public void Handle(JoinGame command)
    {
        throw new NotImplementedException();
    }
}
