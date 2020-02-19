using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Learner.Events
{
    public class LearnerCompetenceDefined : IDomainEvent, ILearnerIdentity
    {
        [JsonConstructor]
        public LearnerCompetenceDefined(LearnerId learnerId, CompetenceId competenceId, string name, bool isRequired)
        {
            LearnerId = learnerId;
            CompetenceId = competenceId;
            Name = name;
            IsRequired = isRequired;
        }
                
        [JsonProperty]
        public LearnerId LearnerId { get; private set; }

        [JsonProperty]
        public CompetenceId CompetenceId { get; private set; }

        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public bool IsRequired { get; private set; }
        [JsonProperty]
        public int Version { get; set; }
    }
}
