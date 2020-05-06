using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Learner
{
    public interface ILearnerRepository
    {
        Task<Maybe<Learner>> LearnerOfId(LearnerId id);

        void Add(Learner theLearner);

        void Update(Learner theLearner);
    }
}
