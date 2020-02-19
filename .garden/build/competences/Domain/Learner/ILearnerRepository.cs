using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Learner
{
    public interface ILearnerRepository
    {
        Task<Learner> Get(LearnerId id);
    }
}
