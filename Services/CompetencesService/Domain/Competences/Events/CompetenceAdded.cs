namespace GuDash.CompetencesService.Domain.Competences.Events
{
    using GuDash.Common.Domain.Model;
    using GuDash.CompetencesService.Domain.Learner;
    using MediatR;
    using Newtonsoft.Json;

    public class CompetenceDefined : IDomainEvent, INotification
    {

        public CompetenceDefined()
        {
        }

        public CompetenceDefined(LearnerId learnerId, CompetenceId competenceId, CompetenceName name, string description, bool isRequired)
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
        public CompetenceName Name { get; private set; }

        [JsonProperty]
        public string Description { get; private set; }

        [JsonProperty]
        public bool IsRequired { get; private set; }

        [JsonProperty]
        public int Version { get; set; } = -1;
    }
}
