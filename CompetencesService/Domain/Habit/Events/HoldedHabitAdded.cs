using System;
using System.Collections.Generic;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;

namespace GuDash.CompetencesService.Domain.Habit
{
    internal class HoldedHabitAdded
    {
        HabitId HabitId { get; }
        public LearnerId LearnerId { get; }
        public CompetenceId CompetenceId { get; }
        public string Name { get; }
        public string Description { get; }
        public TargetData Target { get; }
        public List<DayOfWeek> Progressdays { get; }

        public HoldedHabitAdded(HabitId habitId, LearnerId learnerId, CompetenceId competenceId, string name, string description, TargetData target, List<DayOfWeek> progressdays)
        {
            this.HabitId = habitId;
            this.LearnerId = learnerId;
            this.CompetenceId = competenceId;
            this.Name = name;
            this.Description = description;
            this.Target = target;
            this.Progressdays = progressdays;
        }
    }
}