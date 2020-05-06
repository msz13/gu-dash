using GuDash.Common.Domain.Model;
using System;

namespace GuDash.CompetencesService.Domain.Learner
{
    public class LearnerId : Identity
    {

        public LearnerId(Guid id) : base(id.ToString())
        {

        }

        public LearnerId(string id) : base(id)
        {

        }

        public LearnerId() : base()
        {
        }
    }
}