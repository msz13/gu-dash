using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;
using MediatR;

namespace GuDash.CompetencesService.Domain.Competences.Events
{
    public class CompetenceHabitAdded : IDomainEvent, ICompetenceIdentity, INotification
    {
        public CompetenceHabitAdded(LearnerId learnerId, CompetenceId competenceId, HabitId habitId, string name)
        {
            LearnerId = learnerId;
            CompetenceId = competenceId;
            HabitId = habitId;
            Name = name;
        }

        public LearnerId LearnerId { get; }
        public CompetenceId CompetenceId { get; }
        public HabitId HabitId { get; private set; }
        public string Name { get; private set; }
        public int Version { get; set; }
    }
}
