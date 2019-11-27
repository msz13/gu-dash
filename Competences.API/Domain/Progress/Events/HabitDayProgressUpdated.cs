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
    public class HabitDayProgressUpdated : IDomainEvent, IProgressIdentity, IEvent
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
