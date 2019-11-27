using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.Competences.Service.Domain.Learner;
using GuDash.Common.Domain.Model;
using GuDash.Competences.Service.Domain.Competences;

namespace GuDash.Competences.API.Domain.Competences
{
    public class CompetenceSnapshot 
    {
        public CompetenceSnapshot(CompetenceId competenceId, LearnerId learnerId, string name, string description, bool isActive, bool isRequired, List<CompetenceHabit> habits )
        {
            CompetenceId = competenceId;
            LearnerId = learnerId;
            Name = name;
            Description = description;
            IsActive = isActive;
            IsRequired = isRequired;
            CompetenceHabits = habits;
        }

        public CompetenceId CompetenceId { get; private set; }
        public LearnerId LearnerId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool IsActive { get; private set; }

        public bool IsRequired { get; private set; }

        public List<CompetenceHabit> CompetenceHabits { get; private set; }


    }
}
