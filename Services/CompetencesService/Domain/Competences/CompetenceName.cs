using GuDash.Common.Domain.Model;
using System.Collections.Generic;

namespace GuDash.CompetencesService.Domain.Competences
{
    public class CompetenceName : ValueObject
    {
        public string Value { get; private set; }

        public CompetenceName()
        {
        }

        public CompetenceName(string newName)
        {
            AssertionConcern.AssertArgumentLength(newName, 3, 120, "Competence name length can't be more than 120 characters");
            this.Value = newName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}