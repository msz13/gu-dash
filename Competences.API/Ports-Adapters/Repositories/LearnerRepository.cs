using GuDash.Competences.Service.Domain.Learner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Ports_Adapters.Repositories
{
    public class LearnerRepository : ILearnerRepository
    {
        public async Task<Learner> Get(LearnerId id)
        {
            await Task.Delay(1);
            return new Learner(new LearnerId(), "a@ap.pl", "Europe/Warsaw");
        }
    }
}
