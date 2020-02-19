using CSharpFunctionalExtensions;
using GuDash.Common.Domain.Model;
using Marten;
using NEventStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetencesService.Infrastructure.Persistance
{
    public abstract class MartenEventStoreRepository<TAggregate>
        where TAggregate: IAggregate, new()
    {
        protected IDocumentSession session;

        protected MartenEventStoreRepository(IDocumentSession session)
        {
            this.session = session;
        }

        protected void StartStream(IIdentity id, TAggregate aggregate)
        {
            var pendingEvents = aggregate.GetUncommittedEvents().ToArray();

            session.Events.StartStream(id.Id, pendingEvents);

        }

        protected void UpdateStream(IIdentity id, TAggregate aggregate)
        {
            var pendingEvents = aggregate.GetUncommittedEvents().ToArray();

            session.Events.Append(id.Id, pendingEvents);
        }

        protected async Task<TAggregate> GetOneById(IIdentity id)
        {
            var events = await session.Events.FetchStreamAsync(id.Id, 0);

            if (events.IsEmpty())
                return default;

            var eventsData = events.Select(ev => ev.Data as IDomainEvent).ToList();

            var aggregate= new TAggregate();
            aggregate.LoadEvents(eventsData);

            return aggregate;
        }
    }
}
