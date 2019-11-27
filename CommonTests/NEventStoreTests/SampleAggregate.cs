using NEventStore.Domain;
using NEventStore.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTests.NEventStoreTests
{
    class SampleAggregate : AggregateBase
    {
        public int Value { get; private set; }

        public SampleAggregate(SampleID id, int value) :this(id) {
            
            this.RaiseEvent(new SampleEvent(value));

        }

        public SampleAggregate(SampleID id)
        {
            this.Id = id;
            this.Register<SampleEvent>(this.ApplyOnSampleEvent);

        }

        public void ApplyOnSampleEvent(SampleEvent ev)
        {
            this.Value = ev.Value;
        }

        public override void LoadSnapshot<SampleSnapshot>(SampleSnapshot snapshot)
        {
            throw new NotImplementedException();
        }



    }
}
