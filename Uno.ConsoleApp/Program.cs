using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Commands;
using Uno.Core.QuerySide;
using Uno.Infrastructure;
using Uno.Infrastructure.EventStore;

namespace Uno.ConsoleApp;

public sealed class Program
{
    public static void Main()
    {
        var eventStore = new EventStoreInMemory();
        var repository = new List<CreatingGame>();
        var pendingOrderEventHandler = new CreatingGameEventHandler(repository);
        var eventHandlers = new List<IEventHandler> { pendingOrderEventHandler };
        var eventPublisher = new EventPublisher(eventStore, eventHandlers);
        var commandHandler = new CommandHandler(eventPublisher, eventStore);

        commandHandler.Handle(new CreateGame(new GameId("1")));

        commandHandler.Handle(new CreateGame(new GameId("2")));

        repository.ForEach(Console.WriteLine);
    }
}