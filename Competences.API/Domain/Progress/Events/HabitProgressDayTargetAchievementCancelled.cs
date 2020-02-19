using GuDash.Common.Domain.Model;
using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Domain.Learner;
using NodaTime;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Domain.Progress.Events
{
    public class HabitProgressDayTargetAchievementCancelled : IDomainEvent, IProgressIdentity, IEvent
    {
        public HabitProgressDayTargetAchievementCancelled(ProgressId progressId,
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
