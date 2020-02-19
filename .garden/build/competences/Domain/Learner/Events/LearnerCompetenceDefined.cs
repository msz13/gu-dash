using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Learner.Events
{
    public class LearnerCompetenceDefined : IDomainEvent, ILearnerIdentity
    {
        public LearnerCompetenceDefined(LearnerId learnerId, CompetenceId competenceId, string name, bool isRequired)
        {
            LearnerId = learnerId.Id;
            CompetenceId = competenceId;
            Name = name;
            IsRequired = isRequired;
        }
                
        public string LearnerId { get; }

        public CompetenceId CompetenceId { get; private set; }
        public string Name { get; private set; }
        public bool IsRequired { get; private set; }
        public int Version { get; set; }
    }
}
