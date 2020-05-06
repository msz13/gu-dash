using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;
using NodaTime;


namespace GuDash.CompetencesService.Domain.Progress
{
    public class HabitProgressEnded : IDomainEvent, IProgressIdentity
    {
        public HabitProgressEnded(ProgressId progressId, LearnerId learnerId, HabitId habitId, ZonedDateTime endDate)
        {
            ProgressId = progressId;
            LearnerId = learnerId;
            HabitId = habitId;
            EndDate = endDate;
        }

        public int Version { get; set; }
        public ProgressId ProgressId { get; }
        public LearnerId LearnerId { get; }
        public HabitId HabitId { get; }
        public ZonedDateTime EndDate { get; private set; }

    }
}