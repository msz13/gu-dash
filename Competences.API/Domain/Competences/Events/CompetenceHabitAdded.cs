using GuDash.Common.Domain.Model;
using GuDash.Competences.API.Domain.Competences;
using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Domain.Learner;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Domain.Competences.Events
{
    public class CompetenceHabitAdded : IDomainEvent, ICompetenceIdentity, IEvent
    {
        public CompetenceHabitAdded(LearnerId learnerId, CompetenceId competenceId, HabitId habitId, string name)
        {
            LearnerId = learnerId;
            CompetenceId = competenceId;
            HabitId = habitId;
            Name = name;
        }

        public LearnerId LearnerId { get; }
        public CompetenceId CompetenceId { get; }
        public HabitId  HabitId { get; private set; }
        public string Name { get; private set; }
        public int Version { get; set; }
    }
}
