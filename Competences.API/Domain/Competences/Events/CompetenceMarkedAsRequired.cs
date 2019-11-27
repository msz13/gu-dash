using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.Competences.Service.Domain.Learner;
using GuDash.Common.Domain.Model;
using NServiceBus;
using Newtonsoft.Json;

namespace GuDash.Competences.API.Domain.Competences.Events
{
    public class CompetenceMarkedAsRequired : IDomainEvent, IEvent
    {
        [JsonConstructor]
        public CompetenceMarkedAsRequired(CompetenceId competence, LearnerId learner)
        {
            Competence = competence;
            Learner = learner;
        }

        [JsonProperty]
        public CompetenceId Competence { get; private set; }
        [JsonProperty]
        public LearnerId Learner { get; private set; }

      
        public int Version { get; set; }
    }
}
