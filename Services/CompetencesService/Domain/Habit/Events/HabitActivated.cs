using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using NodaTime;

namespace GuDash.CompetencesService.Domain.Habit.Events
{
    public class HabitActivated : IDomainEvent, IHabitIdentity
    {
        public int Version { get; set; }
        public LearnerId LearnerId { get; private set; }
        public CompetenceId CompetenceId { get; private set; }
        public HabitId HabitId { get; private set; }

        public ZonedDateTime StartActive { get; private set; }

    }
}
