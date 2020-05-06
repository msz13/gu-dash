using CompetencesService.Infrastructure.Persistance;
using CSharpFunctionalExtensions;
using GuDash.CompetencesService.Domain.Learner;
using Marten;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Infrastructure.Persistance
{
    internal class LearnerRepository : MartenEventStoreRepository<Learner>, ILearnerRepository
    {

        public LearnerRepository(IDocumentSession session) : base(session) { }

        public void Add(Learner theLearner)
        {
            this.StartStream(theLearner.LearnerId, theLearner);
        }

        public async Task<Maybe<Learner>> LearnerOfId(LearnerId id)
        {
            return await this.GetOneById(id);
        }

        public void Update(Learner theLearner)
        {
            this.UpdateStream(theLearner.LearnerId, theLearner);
        }
    }
}