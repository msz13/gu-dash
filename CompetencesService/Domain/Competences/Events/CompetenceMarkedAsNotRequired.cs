using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Learner;

namespace GuDash.CompetencesService.Domain.Competences.Events
{
    public class CompetenceMarkedAsNotRequired : IDomainEvent
    {
        public CompetenceMarkedAsNotRequired(CompetenceId competence, LearnerId learner)
        {
            Competence = competence;
            Learner = learner;
        }

        public CompetenceId Competence { get; private set; }
        public LearnerId Learner { get; private set; }
        public int Version { get; set; }
    }
}
