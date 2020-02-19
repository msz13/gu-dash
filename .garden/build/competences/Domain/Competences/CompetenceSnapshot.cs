using System;
using System.Collections.Generic;
using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Learner;
using NEventStore.Domain;

namespace GuDash.CompetencesService.Domain.Competences
{
    public class CompetenceSnapshot: IMemento
    {
        public CompetenceSnapshot(CompetenceId competenceId, LearnerId learnerId, int version, string name, string description, bool isActive, bool isRequired, List<CompetenceHabit> habits)
        {
            CompetenceId = competenceId;
            LearnerId = learnerId;
            Version = version;
            Name = name;
            Description = description;
            IsActive = isActive;
            IsRequired = isRequired;
            CompetenceHabits = habits;
        }

        public int Version { get; set; }

        public CompetenceId CompetenceId { get; private set; }
        public LearnerId LearnerId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool IsActive { get; private set; }

        public bool IsRequired { get; private set; }

        public List<CompetenceHabit> CompetenceHabits { get; private set; }
        public IIdentity Id { get; set; }
        
    }
}
