using GuDash.CompetencesService.Domain.Learner;

namespace GuDash.CompetencesService.Domain.Competences
{
    interface ICompetenceIdentity
    {
        LearnerId LearnerId { get; }
        CompetenceId CompetenceId { get; }

    }
}
