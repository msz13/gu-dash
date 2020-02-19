using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Learner
{
    public class LearnerCompetence : ValueObject
    {
        public LearnerCompetence(CompetenceId competenceId, string name, bool isRequired)
        {
            CompetenceId = competenceId;
            Name = name;
            IsRequired = isRequired;
        }

        public CompetenceId  CompetenceId { get; private set; }

        public string Name { get; private set; }
        public bool IsRequired { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.CompetenceId;
            yield return Name;
            yield return IsRequired;
        }
    }
}
