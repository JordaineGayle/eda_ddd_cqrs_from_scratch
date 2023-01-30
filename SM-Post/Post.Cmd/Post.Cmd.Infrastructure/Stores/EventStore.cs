using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Post.Cmd.Domain.Aggregates;
using Post.Common.EnviromentVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Cmd.Infrastructure.Stores
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventProducer _eventProducer;

        public EventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer)
        {
            _eventStoreRepository = eventStoreRepository;
            _eventProducer = eventProducer;
        }

        public async Task<List<Guid>> GetAggregateIdsAsync()
        {
            var eventStream = await _eventStoreRepository.FindAllAsync();
            if (eventStream == null || !eventStream.Any())
            {
                throw new AggregateNotFoundException("Could not retrieve the event stream from the event store.");
            }

            return eventStream
                .Select(x => x.AggregateIdentifier)
                .Distinct()
                .ToList();
        }

        public async Task<List<BaseEvent?>?> GetEventsAsync(Guid aggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId).ConfigureAwait(false);

            if (eventStream == null || !eventStream.Any()) 
            {
                throw new AggregateNotFoundException("Incorrect post ids provided");
            }


            return eventStream
                .OrderBy(x => x.Version)
                .Select(x => x.EventData)
                .ToList();
        }

        public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId).ConfigureAwait(false);

            if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            {
                throw new ConcurrencyExcepection($"The version number {eventStream[^1].Version} is not matching the expected version {expectedVersion}.");
            }

            var version = expectedVersion;

            foreach(var @event in events)
            {
                version++;
                var newEvent = @event.UpdateVersion(version);
                var eventType = newEvent.GetType().Name;
                var eventModel = new EventModel()
                {
                    TimeStamp = DateTimeOffset.UtcNow.ToUniversalTime(),
                    AggregateIdentifier = aggregateId,
                    AggregateType = nameof(PostAggregate),
                    Version = version,
                    EventType= eventType,
                    EventData = newEvent
                };

                await _eventStoreRepository.SaveAsync(eventModel).ConfigureAwait(false);

                var topic = Environment.GetEnvironmentVariable(EnvironmentVar.KAFKA_TOPIC);

                await _eventProducer.ProduceAsync(topic, @event).ConfigureAwait(false);
            }

        }
    }
}
