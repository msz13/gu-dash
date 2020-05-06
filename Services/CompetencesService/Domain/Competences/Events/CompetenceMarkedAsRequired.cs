using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Learner;
using MediatR;
using Newtonsoft.Json;

namespace GuDash.CompetencesService.Domain.Competences.Events
{
    public class CompetenceMarkedAsRequired : IDomainEvent, INotification
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
