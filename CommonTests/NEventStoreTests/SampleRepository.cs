using NEventStore;
using NEventStore.Domain;
using NEventStore.Domain.Persistence;
using NEventStore.Domain.Persistence.EventStore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommonTests.NEventStoreTests
{
    public class SampleAggrFactory : IConstructAggregates
    {
        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            return new SampleAggregate(new SampleID(id));
        }
    }
    public class SampleRepository : EventStoreRepository
    {
        public SampleRepository(IStoreEvents eventStore, IConstructAggregates factory, IDetectConflicts conflictDetector) : base(eventStore, factory, conflictDetector)
        {
        }
    }
}
