using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Learner
{
    public interface ILearnerRepository
    {
        Task<Learner> LearnerOfId(LearnerId id);

        void Add(Learner theLearner);

        void Update(Learner theLearner);
    }
}
