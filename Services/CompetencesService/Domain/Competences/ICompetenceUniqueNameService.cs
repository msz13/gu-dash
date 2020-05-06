using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using System.Threading.Tasks;

namespace CompetencesService.Domain.Competences
{
    public interface ICompetenceUniqueNameService
    {
        Task<bool> IsUniqueName(LearnerId learnerId, CompetenceName name);
    }
}