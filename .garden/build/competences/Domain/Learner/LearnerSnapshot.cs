using GuDash.CompetencesService.Domain.Learner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Learner
{
    public class LearnerSnapshot
    {
        public LearnerSnapshot(LearnerId learnerId, string email, string timeZoneId, List<LearnerCompetence> learnerCompetences = null)
        {
            LearnerId = learnerId;
            Email = email;
            TimeZoneId = timeZoneId;
            LearnerCompetences = learnerCompetences;
        }

        public LearnerId LearnerId { get; private set; }
        public string Email { get; private set; }

        public string TimeZoneId { get; private set; }

        public List<LearnerCompetence> LearnerCompetences { get; private set; }


    }
}
