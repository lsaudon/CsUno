using System.Collections.Generic;
using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Events;
using Uno.Core.QuerySide;
using Uno.Infrastructure.EventStore;
using Xunit;

namespace Uno.Infrastructure.Tests;

public class EventPublisherTests
{
    [Fact]
    public void Should_Store_Events_When_Publish_Event()
    {
        var eventStore = new EventStoreInMemory();
        var eventPublisher = new EventPublisher(eventStore, new List<IEventHandler>());

        eventPublisher.Publish(new GameCreated(new GameId("1")), 1);

        Assert.Contains(new GameCreated(new GameId("1")), eventStore.GetAll(new GameId("1")));
    }
}