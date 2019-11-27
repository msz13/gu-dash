using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.Competences.Service.Domain.Learner;
using GuDash.Common.Domain.Model;

namespace GuDash.Competences.API.Domain.Competences.Events
{
    public class CompetenceMarkedAsNotRequired :IDomainEvent
    {
        public CompetenceMarkedAsNotRequired(CompetenceId competence, LearnerId learner)
        {
            Competence = competence;
            Learner = learner;
        }

        public CompetenceId Competence { get; private set; }
        public LearnerId Learner { get; private set; }
       public int Version { get; set; }
    }
}
