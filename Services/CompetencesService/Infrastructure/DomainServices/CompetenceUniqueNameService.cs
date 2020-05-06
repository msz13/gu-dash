using CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.CompetencesService.Proto;
using Marten;
using System;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Infrastructure.DomainServices
{
    public class CompetenceUniqueNameService : ICompetenceUniqueNameService
    {
        IQuerySession session;

        public CompetenceUniqueNameService()
        {
        }

        public CompetenceUniqueNameService(IDocumentStore store)
        {
            session = store.QuerySession();
        }


        public async Task<bool> IsUniqueName(LearnerId learnerId, CompetenceName name)
        {
            using (session)
            {
                try
                {
                    var containsName = await session.Query<CompetenceDTO>().AnyAsync(c => c.LearnerId == learnerId.Id && c.Name == name.Value);
                    return containsName ? false : true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }

            }

        }


    }
}
