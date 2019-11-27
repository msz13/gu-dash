using GuDash.Common.Domain.Model;
using GuDash.Competences.API.Domain.Competences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Domain.Learner.Events
{
    public class LearnerCompetenceInitiated : IDomainEvent, ILearnerIdentity
    {
        public LearnerCompetenceInitiated(LearnerId learnerId, CompetenceId competenceId, string name, bool isRequired)
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
