using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using Marten;
using Marten.Events;
using Marten.Events.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Infrastructure.Persistance
{
    public class CompetenceRepository: ICompetenceRepository
    {
        private IDocumentSession session;

        public CompetenceRepository(IDocumentSession session)
        {
            this.session = session;
        }

        public void Add(Competence theCompetence)
        {
            var pendingEvents = theCompetence.GetUncommittedEvents().ToArray();

            session.Events.StartStream(theCompetence.CompetenceId.Id, pendingEvents);



        }

        public CompetenceId NextIdentity()
        {
            return new CompetenceId();
        }


        public async Task<Competence> CompetenceOfId(CompetenceId id)
        {

            var events = await session.Events.FetchStreamAsync(id.Id, 0);
            var eventsData = events.Select(ev => ev.Data as IDomainEvent).ToList();

            var competence = new Competence();
            competence.LoadEvents(eventsData);

            return competence;

        }


    }

}

