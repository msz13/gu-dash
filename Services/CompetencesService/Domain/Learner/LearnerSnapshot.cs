using System.Collections.Generic;

namespace GuDash.CompetencesService.Domain.Learner
{
    public class LearnerSnapshot
    {
        public LearnerSnapshot(LearnerId learnerId, string timeZoneId, List<LearnerCompetence> learnerCompetences = null)
        {
            LearnerId = learnerId;
            TimeZoneId = timeZoneId;
            LearnerCompetences = learnerCompetences;
        }

        public LearnerId LearnerId { get; private set; }

        public string TimeZoneId { get; private set; }

        public List<LearnerCompetence> LearnerCompetences { get; private set; }


    }
}
