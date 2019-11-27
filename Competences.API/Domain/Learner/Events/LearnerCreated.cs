using GuDash.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Domain.Learner
{
    public class LearnerCreated : IDomainEvent, ILearnerIdentity
    {
        public LearnerCreated(LearnerId learnerId, string email, string timeZone) 
       {

            LearnerId = learnerId.Id;
            Email = email;
            TimeZoneId = timeZone;
        }

        public string LearnerId { get; }
   
        public string Email { get;  private set; }
         public string TimeZoneId { get; private set; }
        public int Version { get; set; }
    }
}
