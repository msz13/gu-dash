using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;
using NodaTime;

namespace GuDash.CompetencesService.Domain.Progress.Events
{
    public class HabitProgressDayTargetAchieved : IDomainEvent, IProgressIdentity
    {
        public HabitProgressDayTargetAchieved(
            ProgressId progressId,
            LearnerId learnerId,
            HabitId habitId,
            LocalDate date,
            int dayProgressValue,
            int daysWhenTargetReached)
        {

            ProgressId = progressId;
            LearnerId = learnerId;
            HabitId = habitId;
            Date = date;
            DayProgressValue = dayProgressValue;
            DaysWhenTargetReached = daysWhenTargetReached;
        }

        public int Version { get; set; }
        public ProgressId ProgressId { get; }
        public LearnerId LearnerId { get; }
        public HabitId HabitId { get; }

        public LocalDate Date { get; private set; }

        public int DayProgressValue { get; private set; }

        public int DaysWhenTargetReached { get; private set; }

    }
}
