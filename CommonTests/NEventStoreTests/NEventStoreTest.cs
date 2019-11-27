using GuDash.Competences.API.Ports_Adapters.MongoPersistance;
using GuDash.Competences.Service.Ports_Adapters.Repositories;
using NEventStore;
using NEventStore.Serialization;
using NEventStore.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NEventStore.Domain.Persistence.EventStore;
using CommonTests.NEventStoreTests;
using FluentAssertions;
using NEventStore.Domain.Core;
using System.IO;
using Newtonsoft.Json;

namespace CommonTests
{
    


    public class NEventStoreTest


    {
        [Fact]
        public void InitiateEventStore()
        {
            var eventStore = Wireup.Init()
                .LogToOutputWindow()
                .UsingMongoPersistence("mongodb://test_user:test_2019@ds046037.mlab.com:46037/type_test/?retryWrites=false", new DocumentObjectSerializer())
                .InitializeStorageEngine()
                .Build();

            using (var stream = eventStore.CreateStream(Guid.NewGuid()))
            {
                var @event = new SampleEvent(13);
                stream.Add(new EventMessage { Body = @event });
                stream.CommitChanges(Guid.NewGuid());
                               
            }

            using (var stream = eventStore.CreateStream(Guid.NewGuid()))
            {
                var @event2 = new SampleEvent(20);
                stream.Add(new EventMessage { Body = @event2 });
                stream.CommitChanges(Guid.NewGuid());
            }
        }

        [Fact]
        public void aggregate_should_initiate()
        {
            var id = new SampleID();
            var sample = new SampleAggregate(id, 13);

            sample.Value.Should().Be(13);
            
        }

        [Fact]
        public void repository_should_save_and_load()
        {
            var eventStore = Wireup.Init()
                .LogToOutputWindow()
                .UsingMongoPersistence("mongodb://test_user:test_2019@ds046037.mlab.com:46037/type_test/?retryWrites=false", new DocumentObjectSerializer())
                .InitializeStorageEngine()
                .Build();

            var sampleRepo = new SampleRepository(eventStore, new SampleAggrFactory(), new ConflictDetector());

            var aggrId = new SampleID();
            var samplAggr = new SampleAggregate(aggrId, 13);

            File.WriteAllText(@"d:\programowanie\gu-dash\test1.txt", samplAggr.Version.ToString());

            sampleRepo.Save(samplAggr, Guid.NewGuid());

            var aggr = sampleRepo.GetById<SampleAggregate>(Guid.Parse(aggrId.Id));

            aggr.Value.Should().Be(13);
            aggr.Version.Should().Be(1);

            var json = JsonConvert.SerializeObject(aggr);

            File.AppendAllText(@"d:\programowanie\gu-dash\test1.txt", json);
            
        }
    }
}