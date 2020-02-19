using System.Collections.Generic;
using GuDash.Common.Domain.Model;

namespace GuDash.Competences.Service.Domain.Progress
{
    public class DayProgress : ValueObject
    {
        public DayProgress(bool ifAchievedTarget, int value)
        {
            IfAchievedTarget = ifAchievedTarget;
            Value = value;
        }

        public bool IfAchievedTarget { get; private set; }

        public int Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.IfAchievedTarget;
            yield return this.Value;

        }
    }
}