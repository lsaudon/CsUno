using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Events;
using Uno.Infrastructure.EventStore;
using Xunit;

namespace Uno.Infrastructure.Tests.EventStore;

public class EventStoreInMemoryTests
{
    [Fact]
    public void
        Should_Return_All_Events_When_Get_All_Events_Of_Aggregate_Instance_After_Store_Events_Of_An_Aggregate_Instance()
    {
        var eventStore = new EventStoreInMemory();

        eventStore.Add(new GameCreated(new GameId("1")), 1);
        eventStore.Add(new PlayerJoined(new GameId("1"), new PlayerId("5")), 2);

        var events = eventStore.GetAll(new GameId("1"));

        Assert.Equal(2, events.Count);
    }

    [Fact]
    public void
        Should_Return_Only_Events_Of_Aggregate_Instance_When_Get_All_Events_Of_Aggregate_Instance_After_Store_Events_Of_Several_Aggregate_Instances()
    {
        var eventStore = new EventStoreInMemory();

        eventStore.Add(new GameCreated(new GameId("1")), 1);
        eventStore.Add(new PlayerJoined(new GameId("1"), new PlayerId("5")), 2);
        eventStore.Add(new GameCreated(new GameId("2")), 1);

        var events = eventStore.GetAll(new GameId("1"));

        Assert.Equal(2, events.Count);
    }

    [Fact]
    public void Should_Throw_When_Store_Event_With_Sequence_Event_Already_Stored()
    {
        var eventStore = new EventStoreInMemory();

        eventStore.Add(new GameCreated(new GameId("1")), 1);
        eventStore.Add(new PlayerJoined(new GameId("1"), new PlayerId("5")), 2);

        Assert.Throws<SequenceAlreadyStoredException>(()
            => eventStore.Add(new PlayerJoined(new GameId("1"), new PlayerId("5")), 2));
    }
}
