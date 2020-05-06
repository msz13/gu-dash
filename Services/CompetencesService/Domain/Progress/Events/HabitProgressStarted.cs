using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;
using NodaTime;
using System.Collections.Generic;

namespace GuDash.CompetencesService.Domain.Progress
{
    public class HabitProgressStarted : IDomainEvent, IProgressIdentity
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

        public ITarget Target { get; }

        public List<IsoDayOfWeek> ProgressDays { get; private set; }
        public ZonedDateTime StartActive { get; }
    }
}