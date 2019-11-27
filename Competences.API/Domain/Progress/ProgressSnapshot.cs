using GuDash.Common.Domain.Model;
using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Domain.Learner;
using NEventStore.Domain;
using NodaTime;

namespace GuDash.Competences.Service.Domain.Progress
{
    public  class ProgressSnapshot : IMemento
    {
        public ProgressSnapshot(int version, IIdentity id, LearnerId learnerId, HabitId habitId, ITarget target, ZonedDateTime startActive, int daysWhenTargetReached)
        {
            Version = version;
            Id = id;
            LearnerId = learnerId;
            HabitId = habitId;
            StartActive = startActive;
            Target = target;
            DaysWhenTargetReached = daysWhenTargetReached;
        }

        public int Version { get; set; }
        public IIdentity Id { get; set; }
        public LearnerId LearnerId { get; }
        public HabitId HabitId { get; }
        public ITarget Target { get; }
        public ZonedDateTime StartActive { get; }
        public int DaysWhenTargetReached { get; }

    }
}