using System.Collections.Generic;
using Uno.Core.CommandSide.Commands;
using Uno.Core.CommandSide.Events;

namespace Uno.Core.CommandSide;

public class CommandHandler : ICommandHandler<CreateGame>,
                              ICommandHandler<JoinGame>,
                              ICommandHandler<StartGame>
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

        DecideAndPublish(command, stream);
    }

    public void Handle(JoinGame command)
    {
        var stream = _eventStoreInMemory.GetAll(command.Id);

        DecideAndPublish(command, stream);
    }



    public void Handle(StartGame command)
    {
        var stream = _eventStoreInMemory.GetAll(command.Id);

        DecideAndPublish(command, stream);
    }

    private void DecideAndPublish(IDomainCommand command, IReadOnlyCollection<IDomainEvent> stream)
    {
        var domainEvents = Game.Decide(command, stream);

        foreach (var domainEvent in domainEvents)
        {
            _eventPublisher.Publish(domainEvent, stream.Count + 1);
        }
    }
}
