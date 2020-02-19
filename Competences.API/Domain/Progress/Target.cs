using System.Collections.Generic;
using GuDash.Common.Domain.Model;

namespace GuDash.Competences.Service.Domain.Progress
{
    public class Target 
    {
    }

    public class NumericTarget : ValueObject, ITarget
    {
       private int Value { get;  }

        readonly byte maxValue = byte.MaxValue;
        readonly byte minValue = 0;

        public NumericTarget(int value)
        {
            AssertionConcern.AssertArgumentRange(value, minValue, maxValue, "Target can't be negative or greater than uint");
            Value = value;
        }

        public bool IsAchieved(int value)
        {
            return value >= this.Value;            
        }

        public void ValidateValue(int value)
        {
            AssertionConcern.AssertArgumentRange(value, minValue, maxValue, $"Progress Update Value must be in range {minValue} - {maxValue}");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Value;
        }
                
    }

    public class CheckboxTarget : ValueObject, ITarget
    {
        readonly int value = 1;
        public bool IsAchieved(int value)
        {
           return this.value == value;
        }

        public void ValidateValue(int value)
        {
            AssertionConcern.AssertArgumentEquals(value, 1, "When Progress Target is Checkbox Update Value must be 1");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
        }
    }
}