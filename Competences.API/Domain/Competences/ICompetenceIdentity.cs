using GuDash.Competences.API.Domain.Competences;
using GuDash.Competences.Service.Domain.Learner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Domain.Competences
{
    interface ICompetenceIdentity
    {
        LearnerId LearnerId { get;  }
        CompetenceId CompetenceId { get; }

    }
}
