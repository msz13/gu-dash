using GuDash.CompetencesService.Domain.Learner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Competences
{
    interface ICompetenceIdentity
    {
        LearnerId LearnerId { get;  }
        CompetenceId CompetenceId { get; }

    }
}
