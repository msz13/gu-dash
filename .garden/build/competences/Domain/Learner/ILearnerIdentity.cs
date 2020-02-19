using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Learner
{
    interface ILearnerIdentity
    {
        string LearnerId { get; }
    }
}
