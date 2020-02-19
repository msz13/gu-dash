using GuDash.Common.Domain.Model;
using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Domain.Learner;
using NEventStore.Domain;
using NodaTime;
using System.Collections.Generic;

namespace GuDash.Competences.Service.Domain.Progress
{
    public  class ProgressSnapshot : IMemento
    {
        public ProgressSnapshot(int version, IIdentity id, LearnerId learnerId, HabitId habitId, ITarget target, ZonedDateTime startActive, int daysWhenTargetReached, List<IsoDayOfWeek> progressDays)
        {
            Version = version;
            Id = id;
            LearnerId = learnerId;
            HabitId = habitId;
            StartActive = startActive;
            Target = target;
            DaysWhenTargetReached = daysWhenTargetReached;
            ProgressDays = progressDays;
        }

        public int Version { get; set; }
        public IIdentity Id { get; set; }
        public LearnerId LearnerId { get; }
        public HabitId HabitId { get; }
        public ITarget Target { get; }
        public ZonedDateTime StartActive { get; }

        public List<IsoDayOfWeek> ProgressDays { get; private set; }
        public int DaysWhenTargetReached { get; }

        public Dictionary<LocalDate, DayProgress> DayProgress { get; private set; }

    }
}