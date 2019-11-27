using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Domain.Competences.Events
{
    using GuDash.Competences.API.Domain.Competences;
    using GuDash.Competences.Service.Domain.Learner;
    using GuDash.Common.Domain.Model;
    using NServiceBus;
    using Newtonsoft.Json;

   
    public class CompetenceAdded : IEvent, IDomainEvent
    {

        public CompetenceAdded()
        {
        }

        public CompetenceAdded(LearnerId learnerId, CompetenceId competenceId, string name, string description, bool isRequired)
        {
            LearnerId = learnerId;
            CompetenceId = competenceId;
            Name = name;
            Description = description;
            IsRequired = isRequired;
        }

        [JsonProperty]
        public LearnerId LearnerId { get; private set; }

        [JsonProperty]
        public CompetenceId CompetenceId { get; private set; }

        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public string Description { get; private set; }

        [JsonProperty]
        public bool IsRequired { get; private set; }

        [JsonProperty]
        public int Version { get; set; } = -1;
    }
}
