using GuDash.Common.Domain.Model;
using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Domain.Learner;
using NodaTime;
using NServiceBus;

namespace GuDash.Competences.Service.Domain.Progress
{
    public class HabitProgressStarted : IDomainEvent, IProgressIdentity, IEvent
    {
        public HabitProgressStarted(int version, ProgressId progressId, LearnerId learnerId, HabitId habitId, ITarget target, ZonedDateTime startActive)
        {
            Version = version;
            ProgressId = progressId;
            LearnerId = learnerId;
            HabitId = habitId;
            StartActive = startActive;
            Target = target;
        }

        public int Version { get; set; }
        public ProgressId ProgressId { get; }
        public LearnerId LearnerId { get; }
        public HabitId HabitId { get; }

        public ITarget Target { get;  }
        public ZonedDateTime StartActive { get;  }
    }
}