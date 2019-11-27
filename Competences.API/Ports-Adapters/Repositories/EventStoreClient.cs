using EventStore.ClientAPI;
using GuDash.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Ports_Adapters.Repositories
{
    public class EventStoreClient : IEventStore
    {
        IEventStoreConnection conn;
        public void Connect()
        {
            this.conn = EventStoreConnection.Create(new Uri("tcp://admin:changeit@eventstore:1113"));
            this.conn.ConnectAsync().Wait();
        }

        public void AppendEvents()
        {
            var myEvent = new EventData(Guid.NewGuid(), "testEvent", false,
                            Encoding.UTF8.GetBytes("some data"),
                            Encoding.UTF8.GetBytes("some metadata"));

            conn.AppendToStreamAsync("test-stream", ExpectedVersion.Any, myEvent).Wait();
        }

        public string GetEventStream()
        {
            var readEvents = conn.ReadStreamEventsForwardAsync("testEvent", 0, 10, true).Result;
            var evt = readEvents.Events[0];
            return Encoding.UTF8.GetString(evt.Event.Data);
        }
    }
}
