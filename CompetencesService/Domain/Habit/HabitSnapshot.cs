using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using NEventStore.Domain;
using System;
using System.Collections.Generic;

namespace GuDash.CompetencesService.Domain.Habit
{
    public class HabitSnapshot : IMemento
    {
        public HabitSnapshot(IIdentity id,
            LearnerId learnerId,
            CompetenceId competenceId,
            string name,
            string description,
            Habit.Status status,
            TargetData target,
            List<DayOfWeek> progressDays)
        {
            Id = id;
            LearnerId = learnerId;
            CompetenceId = competenceId;
            Name = name;
            Description = description;
            Status = status;
            Target = target;
            ProgressDays = progressDays;
        }

        public int Version { get; set; }
        
        public IIdentity Id { get; set; }
        public LearnerId LearnerId { get; private set; }
        public CompetenceId CompetenceId { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public Habit.Status Status { get; private set; }
        public TargetData Target { get; private set; }

        public List<DayOfWeek> ProgressDays { get; private set; }

      

    }
}