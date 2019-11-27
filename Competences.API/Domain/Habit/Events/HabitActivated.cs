using GuDash.Common.Domain.Model;
using GuDash.Competences.API.Domain.Competences;
using GuDash.Competences.Service.Domain.Learner;
using NodaTime;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Domain.Habit.Events
{
    public class HabitActivated : IDomainEvent, IHabitIdentity, IEvent
    {
        public int Version { get; set; }
        public LearnerId LearnerId { get; private set; }
        public CompetenceId CompetenceId { get; private set; }
        public HabitId HabitId { get; private set; }

       public  ZonedDateTime StartActive { get; private set; }

    }
}
