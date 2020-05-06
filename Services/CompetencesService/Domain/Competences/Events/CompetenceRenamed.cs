using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using MediatR;

namespace CompetencesService.Domain.Competences.Events
{
    public class CompetenceRenamed : IDomainEvent, ICompetenceIdentity, INotification
    {
        public CompetenceRenamed(LearnerId learnerId, CompetenceId competenceId, CompetenceName newName)
        {
            LearnerId = learnerId;
            CompetenceId = competenceId;
            NewName = newName;
        }

        public int Version { get; set; }
        public LearnerId LearnerId { get; private set; }
        public CompetenceId CompetenceId { get; private set; }

        public CompetenceName NewName { get; private set; }
    }
}
