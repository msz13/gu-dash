using System.Collections.Generic;
using GuDash.Common.Domain.Model;

namespace GuDash.Competences.Service.Domain.Progress
{
    public class Target 
    {
    }

    public class NumericTarget : ValueObject, ITarget
    {
        int Value { get;  }

        public NumericTarget(int value)
        {
            AssertionConcern.AssertArgumentRange(value, 0, uint.MaxValue, "Target can't be negative or greater than uint");
            Value = value;
        }

        public bool IsAchieved(int value)
        {
            return this.Value == value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Value;
        }
    }
}