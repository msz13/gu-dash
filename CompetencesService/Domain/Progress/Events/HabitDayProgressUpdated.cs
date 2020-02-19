using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;
using NodaTime;


namespace GuDash.CompetencesService.Domain.Progress.Events
{
    public class HabitDayProgressUpdated : IDomainEvent, IProgressIdentity
    {
        public HabitDayProgressUpdated(ProgressId progressId, LearnerId learnerId, HabitId habitId, LocalDate date, int dayProgressValue)
        {
            ProgressId = progressId;
            LearnerId = learnerId;
            HabitId = habitId;
            Date = date;
            DayProgressValue = dayProgressValue;
        }

        public int Version { get; set; }
        public ProgressId ProgressId { get; }
        public LearnerId LearnerId { get; }
        public HabitId HabitId { get; }
        public LocalDate Date { get; private set; }
        public int DayProgressValue { get; private set; }

    }
}
