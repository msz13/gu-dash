using GuDash.Common.Domain.Model;
using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Domain.Learner;
using NodaTime;
using NServiceBus;
using System.Collections.Generic;

namespace GuDash.Competences.Service.Domain.Progress
{
    public class HabitProgressStarted : IDomainEvent, IProgressIdentity, IEvent
    {
        public HabitProgressStarted(
            int version,
            ProgressId progressId,
            LearnerId learnerId,
            HabitId habitId,
            ITarget target,
            ZonedDateTime startActive,
            List<IsoDayOfWeek> progressDays)
        {
            Version = version;
            ProgressId = progressId;
            LearnerId = learnerId;
            HabitId = habitId;
            StartActive = startActive;
            Target = target;
            ProgressDays = progressDays;
        }

        public int Version { get; set; }
        public ProgressId ProgressId { get; }
        public LearnerId LearnerId { get; }
        public HabitId HabitId { get; }

        public ITarget Target { get;  }

        public List <IsoDayOfWeek> ProgressDays { get; private set; }
        public ZonedDateTime StartActive { get;  }
    }
}