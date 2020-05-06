using CompetencesService.Infrastructure.Persistance;
using CSharpFunctionalExtensions;
using GuDash.CompetencesService.Domain.Competences;
using Marten;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Infrastructure.Persistance
{
    public class CompetenceRepository : MartenEventStoreRepository<Competence>, ICompetenceRepository
    {


        public CompetenceRepository(IDocumentSession session)
          : base(session)
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

            var result = await this.GetOneById(id)
                .ToResult<Competence>($"Competence of {id} not found")
                .OnFailure(err => throw new KeyNotFoundException(err));
            return result.Value;

        }

        public void Update(Competence theCompetence)
        {
            this.UpdateStream(theCompetence.Id, theCompetence);
        }
    }

}

