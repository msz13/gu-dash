using CompetencesService.Infrastructure.Persistance;
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
    public class CompetenceRepository: MartenEventStoreRepository<Competence>, ICompetenceRepository
    {
        

        public CompetenceRepository(IDocumentSession session)
          :base(session)
        {
            
        }

        public new void Add(Competence theCompetence)
        {
            this.StartStream(theCompetence.Id, theCompetence);          

        }

        public CompetenceId NextIdentity()
        {
            return new CompetenceId();
        }


        public async Task<Competence> CompetenceOfId(CompetenceId id)
        {

            return await this.GetOneById(id);

        }

        public void Update(Competence theCompetence)
        {
            this.UpdateStream(theCompetence.Id, theCompetence);
        }
    }

}

