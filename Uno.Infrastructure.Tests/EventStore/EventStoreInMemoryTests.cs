using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Events;
using Uno.Infrastructure.EventStore;
using Xunit;

namespace Uno.Infrastructure.Tests.EventStore
{
    public class EventStoreInMemoryTests
    {
        [Fact]
        public void
            Should_Return_All_Events_When_Get_All_Events_Of_Aggregate_Instance_After_Store_Events_Of_An_Aggregate_Instance()
        {
            var eventStore = new EventStoreInMemory();

            eventStore.Add(new GameCreated(new GameId("1"), "5"), 1);
            eventStore.Add(new GameOver(new GameId("1"), "5"), 2);

            var events = eventStore.GetAll(new GameId("1"));

            Assert.Equal(2, events.Count);
        }

        [Fact]
        public void
            Should_Return_only_events_of_aggregate_instance_when_get_all_events_of_aggregate_instance_after_store_events_of_several_aggregate_instances()
        {
            var eventStore = new EventStoreInMemory();

            eventStore.Add(new GameCreated(new GameId("1"), "5"), 1);
            eventStore.Add(new GameOver(new GameId("1"), "5"), 2);
            eventStore.Add(new GameCreated(new GameId("2"), "5"), 1);

            var events = eventStore.GetAll(new GameId("1"));

            Assert.Equal(2, events.Count);
        }

        [Fact]
        public void Should_throw_when_store_event_with_sequence_event_already_stored()
        {
            var eventStore = new EventStoreInMemory();

            eventStore.Add(new GameCreated(new GameId("1"), "5"), 1);
            eventStore.Add(new GameOver(new GameId("1"), "5"), 2);

            Assert.Throws<SequenceAlreadyStoredException>(()
                => eventStore.Add(new GameOver(new GameId("1"), "5"), 2));
        }
    }
}
