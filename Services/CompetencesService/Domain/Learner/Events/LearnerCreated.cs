using GuDash.Common.Domain.Model;
using Newtonsoft.Json;

namespace GuDash.CompetencesService.Domain.Learner
{
    public class LearnerCreated : IDomainEvent, ILearnerIdentity
    {
        [JsonConstructor]
        public LearnerCreated(LearnerId learnerId, string timeZone)
        {

            LearnerId = learnerId;
            TimeZoneId = timeZone;
        }

        [JsonProperty]
        public LearnerId LearnerId { get; private set; }

        [JsonProperty]
        public string TimeZoneId { get; private set; }

        [JsonProperty]
        public int Version { get; set; }
    }
}
