using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.Competences.API.Domain.Competences;
using GuDash.Competences.API.Domain.Competences.Events;
using GuDash.Competences.Service.Domain.Competences.Events;
using NServiceBus;

namespace GuDash.Competences.API.Ports_Adapters.Repositories
{
    
    public class CompetencesRepostiory : ICompetencesRepository
    {
        IEndpointInstance bus;

        public CompetencesRepostiory(IEndpointInstance bus)
        {
            this.bus = bus;
        }

        public async Task Add(Competence theCompetence)
        {
            var ev = theCompetence.GetChanges()[0];
            //var ev = new SEvent(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            //var ev = new CompetenceMarkedAsRequired(new CompetenceId(Guid.NewGuid()), new Service.Domain.Learner.LearnerId(Guid.NewGuid()));

            await bus.Publish(ev);
        }
    }
}
