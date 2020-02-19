using GuDash.Common.Domain.Model;
using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Domain.Learner;
using NodaTime;
using NServiceBus;

namespace GuDash.Competences.Service.Domain.Progress
{
    public class HabitProgressEnded : IDomainEvent, IProgressIdentity, IEvent
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