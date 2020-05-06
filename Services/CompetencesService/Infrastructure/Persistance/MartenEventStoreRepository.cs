using CSharpFunctionalExtensions;
using GuDash.Common.Domain.Model;
using Marten;
using NEventStore.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace CompetencesService.Infrastructure.Persistance
{
    public abstract class MartenEventStoreRepository<TAggregate>
        where TAggregate : IAggregate, new()
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

            aggregate.ClearUncommittedEvents();

        }

        protected void UpdateStream(IIdentity id, TAggregate aggregate)
        {
            var pendingEvents = aggregate.GetUncommittedEvents().ToArray();

            session.Events.Append(id.Id, pendingEvents);

            aggregate.ClearUncommittedEvents();
        }

        protected async Task<Maybe<TAggregate>> GetOneById(IIdentity id)
        {
            var events = await session.Events.FetchStreamAsync(id.Id, 0);

            if (events.IsEmpty())
                return new Maybe<TAggregate>();

            var eventsData = events.Select(ev => ev.Data as IDomainEvent).ToList();

            var aggregate = new TAggregate();
            aggregate.LoadEvents(eventsData);

            return aggregate;
        }
    }
}
